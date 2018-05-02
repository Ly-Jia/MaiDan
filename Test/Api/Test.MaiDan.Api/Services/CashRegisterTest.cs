using MaiDan.Api.Services;
using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using Moq;
using NFluent;
using NUnit.Framework;
using System;
using System.Linq;
using Test.MaiDan.Billing;
using Test.MaiDan.Ordering;
using ADish = Test.MaiDan.Ordering.ADish;

namespace Test.MaiDan.Api.Services
{
    [TestFixture]
    public class CashRegisterTest
    {
        [Test]
        public void should_print_a_bill_from_an_order()
        {
            var cashRegister = new CashRegister(new Mock<IRepository<Dish>>().Object, new Mock<IRepository<global::MaiDan.Ordering.Domain.Order>>().Object, new Mock<IRepository<Bill>>().Object, new ATaxConfiguration().Build());
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

            var cashRegister = new CashRegister(menu.Object, new Mock<IRepository<global::MaiDan.Ordering.Domain.Order>>().Object, new Mock<IRepository<Bill>>().Object, new ATaxConfiguration().Build());
            
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

            var cashRegister = new CashRegister(menu.Object, new Mock<IRepository<global::MaiDan.Ordering.Domain.Order>>().Object, new Mock<IRepository<Bill>>().Object, new ATaxConfiguration().Build());

            var bill = cashRegister.Calculate(order);
            
            Check.That(bill.Total).Equals(25m);
        }

        [Test]
        public void should_determine_tax_as_reduced()
        {
            var order = new AnOrder()
                .With(1, new ADish("1").Build())
                .Build();

            var taxRateAt10Percent = 10m;
            var menu = new Mock<IRepository<Dish>>();
            var fiveEuros = 5m;
            menu.Setup(m => m.Get("1")).Returns(new Billing.ADish("1").Priced(fiveEuros).OfType("Starter").Build());
            
            var cashRegister = new CashRegister(menu.Object, new Mock<IRepository<global::MaiDan.Ordering.Domain.Order>>().Object, new Mock<IRepository<Bill>>().Object, new ATaxConfiguration().Build());

            //S'assurer que la taxe est bien celle réduite, pas au montant suelement
            var bill = cashRegister.Calculate(order);

            Check.That(bill.Lines.ElementAt(0).TaxRate.Rate).Equals(taxRateAt10Percent);
        }

        [Test]
        public void should_determine_tax_as_regular()
        {
            var order = new AnOrder()
                .With(1, new ADish("1").Build())
                .Build();

            var taxRateAt20Percent = 20m;
            var menu = new Mock<IRepository<Dish>>();
            var fiveEuros = 5m;
            menu.Setup(m => m.Get("1")).Returns(new Billing.ADish("1").Priced(fiveEuros).OfType("Alcool").Build());

            var cashRegister = new CashRegister(menu.Object, new Mock<IRepository<global::MaiDan.Ordering.Domain.Order>>().Object, new Mock<IRepository<Bill>>().Object, new ATaxConfiguration().Build());

            var bill = cashRegister.Calculate(order);

            //S'assurer que la taxe est bien celle normale (par l'id?), pas au montant suelement
            Check.That(bill.Lines.ElementAt(0).TaxRate.Rate).Equals(taxRateAt20Percent);
        }

        [Test]
        public void should_not_print_a_lineless_order()
        {
            var order = new AnOrder()
                .Build();

            var cashRegister = new CashRegister(new Mock<IRepository<Dish>>().Object, new Mock<IRepository<global::MaiDan.Ordering.Domain.Order>>().Object, new Mock<IRepository<Bill>>().Object, new Mock<IRepository<Tax>>().Object);

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

            var cashRegister = new CashRegister(menu.Object, new Mock<IRepository<global::MaiDan.Ordering.Domain.Order>>().Object, new Mock<IRepository<Bill>>().Object, new ATaxConfiguration().Build());

            var bill = cashRegister.Calculate(order);

            var tenPercentTaxAmount = bill.Taxes.Single(t => t.TaxRate.Rate == 10m);
            Check.That(tenPercentTaxAmount.Amount).Equals(2m);
            var twentyPercentTaxAmount = bill.Taxes.Single(t => t.TaxRate.Rate == 20m);
            Check.That(twentyPercentTaxAmount.Amount).Equals(3m);
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

            var cashRegister = new CashRegister(menu.Object, new Mock<IRepository<global::MaiDan.Ordering.Domain.Order>>().Object, new Mock<IRepository<Bill>>().Object, new ATaxConfiguration().Build());

            var bill = cashRegister.Calculate(order);

            Check.That(bill.Taxes.First().Amount).Equals(2.5m); // instead of 2,49 €
        }
    }
}
