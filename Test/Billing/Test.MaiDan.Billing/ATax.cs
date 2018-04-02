using System;
using System.Collections.Generic;
using MaiDan.Billing.Domain;

namespace Test.MaiDan.Billing
{
    public class ATax
    {
        public static string DEFAULT_TAX_ID = "DEF";
        private string id = DEFAULT_TAX_ID;
        public static IList<TaxRate> DEFAULT_TAX_CONF = new List<TaxRate>{new TaxRate(10m, new DateTime(2012,12,12), DateTime.MaxValue)};
        private IList<TaxRate> taxConfig = DEFAULT_TAX_CONF;
            
        public Tax Build()
        {
            return new Tax(id, taxConfig);
        }
    }
}
