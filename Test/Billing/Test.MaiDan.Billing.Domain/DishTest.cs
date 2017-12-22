using NFluent;
using NUnit.Framework;
using System;

namespace Test.MaiDan.Billing.Domain
{
    [TestFixture]
    public class DishTest
    {
        [Test]
        public void should_be_priced_with_the_latest_configured_price()
        {
            var applicationStartDate = new DateTime(2011, 01, 01);
            var priceChangeDate = new DateTime(2012, 01, 01);
            var dish = new ADish().Priced(5m, priceChangeDate).And().Priced(1m, applicationStartDate, priceChangeDate.AddDays(-1)).Build();

            Check.That(dish.CurrentPrice).Equals(5m);
        }

        [Test]
        public void should_be_equal_when_id_and_price_configuration_are_the_same()
        {
            var dish1 = new ADish("Id").Priced(5m).Build();
            var dish2 = new ADish("Id").Priced(5m).Build();

            var isEqual = dish1.Equals(dish2);

            Check.That(isEqual).IsTrue();
        }

        [Test]
        public void should_not_be_equal_when_id_is_not_the_same()
        {
            var dish1 = new ADish("Id1").Priced(5m).Build();
            var dish2 = new ADish("Id2").Priced(5m).Build();

            var isEqual = dish1.Equals(dish2);

            Check.That(isEqual).IsFalse();
        }

        [Test]
        public void should_be_equal_when_price_configuration_is_not_the_same()
        {
            var dish1 = new ADish("Id").Priced(5m).Build();
            var dish2 = new ADish("Id").Priced(6m).Build();

            var isEqual = dish1.Equals(dish2);

            Check.That(isEqual).IsFalse();
        }
    }
}
