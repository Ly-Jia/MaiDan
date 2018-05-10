using MaiDan.Infrastructure.Database;
using Moq;
using NFluent;
using NUnit.Framework;
using System;
using System.Linq;
using Test.MaiDan.Billing;
using Test.MaiDan.Ordering;
using ADish = Test.MaiDan.Ordering.ADish;
using Dish = MaiDan.Billing.Domain.Dish;

namespace Test.MaiDan.Api.Services
{
    [TestFixture]
    public class CashRegisterTest
    {
        [Test]
        public void should_print_a_bill_from_an_order()
        {
            var cashRegister = new ACashRegister().Build();
            var order = new AnOrder().Build();

            var bill = cashRegister.Calculate(order);

            Check.That(bill.Id).Equals(order.Id);
        }

        [Test]
        public void should_calculate_lines_amount_using_the_menu()
        {
            var order = new AnOrder()
                .With(1, new ADish("1").Build())
                .With(2, new ADish("2").Build())
                .Build();

            var menu = new Mock<IRepository<Dish>>();
            menu.Setup(m => m.Get("1")).Returns(new Billing.ADish("1").Priced(5m).Build());
            menu.Setup(m => m.Get("2")).Returns(new Billing.ADish("2").Priced(10m).Build());

            var cashRegister = new ACashRegister(menu.Object).Build();
            
            var bill = cashRegister.Calculate(order);
            
            Check.That(bill.Lines.ElementAt(0).Amount).Equals(5m);
            Check.That(bill.Lines.ElementAt(1).Amount).Equals(20m);
        }

        [Test]
        public void should_calculate_bill_total()
        {
            var order = new AnOrder()
                .With(1, new ADish("1").Build())
                .With(2, new ADish("2").Build())
                .Build();

            var menu = new Mock<IRepository<Dish>>();
            menu.Setup(m => m.Get("1")).Returns(new Billing.ADish("1").Priced(5m).Build());
            menu.Setup(m => m.Get("2")).Returns(new Billing.ADish("2").Priced(10m).Build());

            var cashRegister = new ACashRegister(menu.Object).Build();

            var bill = cashRegister.Calculate(order);
            
            Check.That(bill.Total).Equals(25m);
        }

        [Test]
        public void should_determine_tax_as_reduced_when_dish_is_not_of_type_alcohol()
        {
            var order = new AnOrder()
                .With(1, new ADish("1").Build())
                .Build();

            var taxRateAt10Percent = 0.10m;
            var menu = new Mock<IRepository<Dish>>();
            var fiveEuros = 5m;
            menu.Setup(m => m.Get("1")).Returns(new Billing.ADish("1").Priced(fiveEuros).OfType("Starter").Build());
            
            var cashRegister = new ACashRegister(menu.Object).Build();

            //S'assurer que la taxe est bien celle réduite, pas au montant suelement
            var bill = cashRegister.Calculate(order);

            Check.That(bill.Lines.ElementAt(0).TaxRate.Rate).Equals(taxRateAt10Percent);
        }

        [Test]
        public void should_determine_tax_as_regular_when_dish_is_of_type_alcohol()
        {
            var order = new AnOrder()
                .With(1, new ADish("1").Build())
                .Build();

            var taxRateAt20Percent = 0.20m;
            var menu = new Mock<IRepository<Dish>>();
            var fiveEuros = 5m;
            menu.Setup(m => m.Get("1")).Returns(new Billing.ADish("1").Priced(fiveEuros).OfType("Alcool").Build());

            var cashRegister = new ACashRegister(menu.Object).Build();

            var bill = cashRegister.Calculate(order);

            //S'assurer que la taxe est bien celle normale (par l'id?), pas au montant seulement
            Check.That(bill.Lines.ElementAt(0).TaxRate.Rate).Equals(taxRateAt20Percent);
        }

        [Test]
        public void should_not_print_a_lineless_order()
        {
            var order = new AnOrder()
                .Build();

            var cashRegister = new ACashRegister().Build();

            Check.ThatCode(() => cashRegister.Print(order)).Throws<InvalidOperationException>();
        }

        [Test]
        public void should_group_amounts_by_tax_in_the_bill()
        {
            var beer = "Beer";
            var wine = "Wine";
            var starter = "Starter";
            var dessert = "Dessert";

            var order = new AnOrder()
                .With(1, new ADish(beer).Build())
                .With(1, new ADish(wine).Build())
                .With(1, new ADish(starter).Build())
                .With(1, new ADish(dessert).Build())
                .Build();
            
            var menu = new Mock<IRepository<Dish>>();
            menu.Setup(m => m.Get(beer)).Returns(new Billing.ADish(beer).Priced(6m).OfType("Alcool").Build()); // Tax (20%) of 1 €
            menu.Setup(m => m.Get(wine)).Returns(new Billing.ADish(wine).Priced(12m).OfType("Alcool").Build()); // Tax (20%) of 2 €
            menu.Setup(m => m.Get(starter)).Returns(new Billing.ADish(starter).Priced(11m).OfType("Starter").Build()); // Tax (10%) of 1 €
            menu.Setup(m => m.Get(dessert)).Returns(new Billing.ADish(dessert).Priced(11m).OfType("Dessert").Build()); // Tax (10%) of 1 €

            var taxConfiguration = new ATaxConfiguration().Build();
            var cashRegister = new ACashRegister(menu.Object, taxConfiguration).Build();

            var bill = cashRegister.Calculate(order);

            var tenPercentTaxAmount = bill.Taxes[taxConfiguration.Get("RED").TaxConfiguration[0]];
            Check.That(tenPercentTaxAmount).Equals(2m);
            var twentyPercentTaxAmount = bill.Taxes[taxConfiguration.Get("REG").TaxConfiguration[0]];
            Check.That(twentyPercentTaxAmount).Equals(3m);
        }
        
        [Test]
        public void should_calculate_bill_tax_from_the_sum_of_lines_amount()
        {
            var cocktail = new ADish("Cocktail").Build();

            var order = new AnOrder()
                .With(1, cocktail)
                .With(1, cocktail)
                .With(1, cocktail)
                .Build();

            var menu = new Mock<IRepository<Dish>>();
            menu.Setup(m => m.Get(cocktail.Id)).Returns(new Billing.ADish(cocktail.Id).Priced(5m).OfType("Alcool").Build()); // Tax (20%) of 0,83 €

            var taxConfiguration = new ATaxConfiguration().Build();
            var cashRegister = new ACashRegister(menu.Object, taxConfiguration).Build();

            var bill = cashRegister.Calculate(order);

            var tax_amount = bill.Taxes[taxConfiguration.Get("REG").TaxConfiguration[0]];
            Check.That(tax_amount).Equals(2.5m); // instead of 2,49 €
        }

        [Test]
        public void should_not_apply_discount_for_on_site_bills()
        {
            var dish = new ADish().Build();
            var order = new AnOrder()
                .OnSite()
                .With(1, dish)
                .Build();

            var menu = new Mock<IRepository<Dish>>();
            menu.Setup(m => m.Get(dish.Id)).Returns(new Billing.ADish(dish.Id).Priced(10m).Build());

            var taxConfiguration = new ATaxConfiguration().Build();
            var cashRegister = new ACashRegister(menu.Object, taxConfiguration).Build();

            var bill = cashRegister.Calculate(order);

            Check.That(bill.Total).Equals(10m);
            var tenPercentTaxAmount = bill.Taxes[taxConfiguration.Get("RED").TaxConfiguration[0]];
            Check.That(tenPercentTaxAmount).Equals(0.91m);
        }

        [Test]
        public void should_apply_a_ten_percent_discount_on_the_reduced_tax_items_of_a_take_away_bill()
        {
            var dish = new ADish().Build();
            var order = new AnOrder()
                .TakeAway()
                .With(1, dish)
                .Build();

            var menu = new Mock<IRepository<Dish>>();
            menu.Setup(m => m.Get(dish.Id)).Returns(new Billing.ADish(dish.Id).Priced(10m).WithReducedTax().Build());

            var taxConfiguration = new ATaxConfiguration().Build();
            var cashRegister = new ACashRegister(menu.Object, taxConfiguration).Build();

            var bill = cashRegister.Calculate(order);

            var discount = bill.Discounts.First();
            Check.That(discount.Key.Rate).Equals(0.10m);
            Check.That(discount.Value).Equals(1m);
            Check.That(bill.Total).Equals(9m);
            var tenPercentTaxAmount = bill.Taxes[taxConfiguration.Get("RED").TaxConfiguration[0]];
            Check.That(tenPercentTaxAmount).Equals(0.82m);
        }

        [Test]
        public void should_not_alter_lines_amount_when_a_take_away_discount_is_applied()
        {
            var dish = new ADish().Build();
            var order = new AnOrder()
                .TakeAway()
                .With(1, dish)
                .Build();

            var menu = new Mock<IRepository<Dish>>();
            menu.Setup(m => m.Get(dish.Id)).Returns(new Billing.ADish(dish.Id).Priced(10m).WithReducedTax().Build());

            var cashRegister = new ACashRegister(menu.Object).Build();

            var bill = cashRegister.Calculate(order);

            Check.That(bill.Lines.First().Amount).Equals(10m);
        }

        [Test]
        public void should_not_apply_the_ten_percent_discount_on_regular_tax_products_in_take_away_bills()
        {
            var dish = new ADish().Build();
            var order = new AnOrder()
                .TakeAway()
                .With(1, dish)
                .Build();

            var menu = new Mock<IRepository<Dish>>();
            menu.Setup(m => m.Get(dish.Id)).Returns(new Billing.ADish(dish.Id).Priced(10m).WithRegularTax().Build());

            var cashRegister = new ACashRegister(menu.Object).Build();

            var bill = cashRegister.Calculate(order);

            Check.That(bill.Discounts).IsEmpty();
            Check.That(bill.Total).Equals(10m);
        }
    }
}
