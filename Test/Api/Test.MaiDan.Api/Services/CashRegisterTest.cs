﻿using System.Collections.Generic;
using MaiDan.Api.Services;
using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using Moq;
using NFluent;
using NUnit.Framework;
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
            var cashRegister = new CashRegister(new Mock<IRepository<Dish>>().Object, new Mock<IRepository<Bill>>().Object, new ATaxConfiguration().Build());
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

            var cashRegister = new CashRegister(menu.Object, new Mock<IRepository<Bill>>().Object, new ATaxConfiguration().Build());
            
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

            var cashRegister = new CashRegister(menu.Object, new Mock<IRepository<Bill>>().Object, new ATaxConfiguration().Build());

            var bill = cashRegister.Calculate(order);
            
            Check.That(bill.Total).Equals(3m);
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
            
            var cashRegister = new CashRegister(menu.Object, new Mock<IRepository<Bill>>().Object, new ATaxConfiguration().Build());

            //S'assurer que la taxe est bien celle réduite, pas au montant suelement
            var bill = cashRegister.Calculate(order);

            Check.That(bill.Lines.ElementAt(0).TaxRate.Rate).Equals(taxRateAt10Percent);
            var expectedTaxAmount = (fiveEuros * 100) / (100 + taxRateAt10Percent);
            Check.That(bill.Lines.ElementAt(0).TaxAmount).Equals(expectedTaxAmount);
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

            var cashRegister = new CashRegister(menu.Object, new Mock<IRepository<Bill>>().Object, new ATaxConfiguration().Build());

            var bill = cashRegister.Calculate(order);

            //S'assurer que la taxe est bien celle normale (par l'id?), pas au montant suelement
            Check.That(bill.Lines.ElementAt(0).TaxRate.Rate).Equals(taxRateAt20Percent);
            var expectedTaxAmount = (fiveEuros * 100) / (100 + taxRateAt20Percent);
            Check.That(bill.Lines.ElementAt(0).TaxAmount).Equals(expectedTaxAmount);
        }


    }
}
