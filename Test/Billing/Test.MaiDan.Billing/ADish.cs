using System;
using System.Collections.Generic;
using MaiDan.Billing.Domain;

namespace Test.MaiDan.Billing
{
    public class ADish
    {
        private static readonly string DEFAULT_ID = "1";
        private string id;
        private List<Price> priceConfiguration = new List<Price>();

        public ADish() : this(DEFAULT_ID)
        {
            
        }

        public ADish(string id)
        {
            this.id = id;
        }

        public Dish Build()
        {
            return new Dish(id, priceConfiguration);
        }

        public ADish Priced(decimal amount, DateTime validFrom = default(DateTime), DateTime validTo = default(DateTime))
        {
            priceConfiguration.Add(new Price(amount, validFrom, validTo));
            return this;
        }

    }
}
