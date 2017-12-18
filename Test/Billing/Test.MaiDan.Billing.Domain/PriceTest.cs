using System;
using MaiDan.Billing.Domain;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Billing.Domain
{
    [TestFixture]
    public class PriceTest
    {
        [Test]
        public void shoul_be_equal_when_validity_and_amount_are_the_same()
        {
            var amount = 5m;
            var validityStartDate = new DateTime(2012, 01, 01);
            var validityEndDate = new DateTime(2013,01,01);
            var price1 = new Price(amount, validityStartDate, validityEndDate);
            var price2 = new Price(amount, validityStartDate, validityEndDate);

            var isEqual = price1.Equals(price2);

            Check.That(isEqual).IsTrue();
        }

        [Test]
        public void shoul_not_be_equal_when_amount_is_not_the_same()
        {
            var validityStartDate = new DateTime(2012, 01, 01);
            var validityEndDate = new DateTime(2013, 01, 01);
            var price1 = new Price(5m, validityStartDate, validityEndDate);
            var price2 = new Price(6m, validityStartDate, validityEndDate);

            var isEqual = price1.Equals(price2);

            Check.That(isEqual).IsFalse();
        }

        [Test]
        public void should_not_be_equal_when_validity_start_date_is_not_the_same()
        {
            decimal amount = 5;
            var validityEndDate = new DateTime(2013, 01, 01);
            var price1 = new Price(amount, new DateTime(2012,01,01), validityEndDate);
            var price2 = new Price(amount, new DateTime(2011,01,01), validityEndDate);

            var isEqual = price1.Equals(price2);

            Check.That(isEqual).IsFalse();
        }

        [Test]
        public void should_not_be_equal_when_validity_end_date_is_not_the_same()
        {
            decimal amount = 5;
            var validityStartDate = new DateTime(2012, 01, 01);
            var price1 = new Price(amount, validityStartDate, new DateTime(2013,01,01));
            var price2 = new Price(amount, validityStartDate, new DateTime(2014,01,01));

            var isEqual = price1.Equals(price2);

            Check.That(isEqual).IsFalse();
        }
    }
}
