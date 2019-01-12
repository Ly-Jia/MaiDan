using System;
using System.Collections.Generic;
using MaiDan.Billing.Domain;

namespace Test.MaiDan.Billing
{
    public class ATax
    {
        private const string DefaultTaxId = "DEF";
        public static readonly List<TaxRate> DefaultTaxConf = new List<TaxRate>();
        public static readonly Tax DefaultTax = new Tax(DefaultTaxId, DefaultTaxConf);
        public static readonly TaxRate DefaultTaxRate = new TaxRate("DEF-1", DefaultTax, 10m, new DateTime(2012, 12, 12), DateTime.MaxValue);

        public ATax()
        {
            DefaultTax.TaxConfiguration.Add(DefaultTaxRate);
        }

        public Tax Build()
        {
            return DefaultTax;
        }
    }
}
