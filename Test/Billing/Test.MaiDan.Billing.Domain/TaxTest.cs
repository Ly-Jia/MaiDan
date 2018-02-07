using System;
using System.Collections.Generic;
using MaiDan.Billing.Domain;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Billing.Domain
{
    public class TaxTest
    {
        [Test]
        public void should_be_priced_with_the_latest_configured_price()
        {
            var applicationStartDate = new DateTime(2011, 01, 01);
            var priceChangeDate = new DateTime(2012, 01, 01);
            var tax = new Tax("RED", new List<TaxRate>
            {
                new TaxRate(5.5m, applicationStartDate, priceChangeDate.AddDays(-1)),
                new TaxRate(10m, priceChangeDate, DateTime.MinValue)
            });

            Check.That(tax.CurrentRate).Equals(10m);
        }
    }
}
