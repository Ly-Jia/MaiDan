using MaiDan.Api.Services;
using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using Moq;
using NFluent;
using NUnit.Framework;
using System.Linq;
using Test.MaiDan.Ordering;

namespace Test.MaiDan.Api.Services
{
    [TestFixture]
    public class CashRegisterTest
    {
        [Test]
        public void should_print_a_bill_from_an_order()
        {
            var cashRegister = new CashRegister(new Mock<IRepository<Dish>>().Object);
            var order = new AnOrder().Build();

            var bill = cashRegister.Print(order);

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

            var cashRegister = new CashRegister(menu.Object);


            var bill = cashRegister.Print(order);


            Check.That(bill.Lines.ElementAt(0).Amount).Equals(5m);
            Check.That(bill.Lines.ElementAt(1).Amount).Equals(20m);

        }
        
    }
}
