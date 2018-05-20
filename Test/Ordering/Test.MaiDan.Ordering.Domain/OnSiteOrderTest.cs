using MaiDan.Ordering.Domain;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Ordering.Domain
{
    [TestFixture]
    public class OnSiteOrderTest
    {

        [Test]
        public void should_be_equal_when_orders_are_the_same()
        {
            var id = 1;
            Table table = new Table("T1");
            int numberOfGuests = 2;

            var order1 = new AnOrder(id).OnSite(table, numberOfGuests).Build();
            var order2 = new AnOrder(id).OnSite(table, numberOfGuests).Build();

            var isEqual = order1.Equals(order2);

            Check.That(isEqual).IsTrue();
        }

        [Test]
        public void should_not_be_equal_when_table_is_not_the_same()
        {
            var id = 1;
            int numberOfGuests = 2;

            var order1 = new AnOrder(id).OnSite("T1", numberOfGuests).Build();
            var order2 = new AnOrder(id).OnSite("T2", numberOfGuests).Build();

            var isEqual = order1.Equals(order2);

            Check.That(isEqual).IsFalse();
        }

        [Test]
        public void should_not_be_equal_when_number_of_guests_are_not_the_same()
        {
            var id = 1;
            Table table = new Table("T1");

            var order1 = new AnOrder(id).OnSite(table, 1).Build();
            var order2 = new AnOrder(id).OnSite(table, 2).Build();

            var isEqual = order1.Equals(order2);

            Check.That(isEqual).IsFalse();
        }
    }
}
