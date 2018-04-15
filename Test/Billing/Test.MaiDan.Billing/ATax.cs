using System;
using System.Collections.Generic;
using MaiDan.Billing.Domain;

namespace Test.MaiDan.Billing
{
    public class ATax
    {
        public static string DEFAULT_TAX_ID = "DEF";

        public static List<TaxRate> DEFAULT_TAX_CONF = new List<TaxRate>();
        public static Tax DEFAULT_TAX = new Tax(DEFAULT_TAX_ID, DEFAULT_TAX_CONF);
        public static TaxRate DEFAULT_TAX_RATE = new TaxRate("DEF-1", DEFAULT_TAX, 10m, new DateTime(2012, 12, 12), DateTime.MinValue);

        public ATax()
        {
            DEFAULT_TAX.TaxConfiguration.Add(DEFAULT_TAX_RATE);
        }

        public Tax Build()
        {
            return DEFAULT_TAX;
        }
    }
}
