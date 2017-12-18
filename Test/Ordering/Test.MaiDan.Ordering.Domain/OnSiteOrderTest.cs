using System.Collections.Generic;
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
            var id = "id";
            Table table = new Table("T1");
            int numberOfGuests = 2;

            var order1 = new OnSiteOrder(id, table, numberOfGuests, new List<Line>());
            var order2 = new OnSiteOrder(id, table, numberOfGuests, new List<Line>());

            var isEqual = order1.Equals(order2);

            Check.That(isEqual).IsTrue();
        }

        [Test]
        public void should_not_be_equal_when_table_is_not_the_same()
        {
            var id = "id";
            int numberOfGuests = 2;

            var order1 = new OnSiteOrder(id, new Table("T1"), numberOfGuests, new List<Line>());
            var order2 = new OnSiteOrder(id, new Table("T2"), numberOfGuests, new List<Line>());

            var isEqual = order1.Equals(order2);

            Check.That(isEqual).IsFalse();
        }

        [Test]
        public void should_not_be_equal_when_number_of_guests_are_not_the_same()
        {
            var id = "id";
            Table table = new Table("T1");

            var order1 = new OnSiteOrder(id, table, 1, new List<Line>());
            var order2 = new OnSiteOrder(id, table, 2, new List<Line>());

            var isEqual = order1.Equals(order2);

            Check.That(isEqual).IsFalse();
        }
    }
}
