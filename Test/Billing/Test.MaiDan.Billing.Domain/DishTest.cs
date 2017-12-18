using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Billing.Domain
{
    [TestFixture]
    public class DishTest
    {
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
