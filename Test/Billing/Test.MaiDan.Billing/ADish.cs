﻿using System;
using System.Collections.Generic;
using MaiDan.Billing.Domain;

namespace Test.MaiDan.Billing
{
    public class ADish
    {
        private static readonly string DEFAULT_ID = "1";
        private string id;
        private List<Price> priceConfiguration = new List<Price>();
        private string type = "Starter";

        public ADish() : this(DEFAULT_ID)
        {
        }

        public ADish(string id)
        {
            this.id = id;
        }

        public Dish Build()
        {
            return new Dish(id, priceConfiguration, type);
        }

        public ADish Priced(decimal amount)
        {
            return Priced(amount, DateTime.MinValue, DateTime.MaxValue);
        }

        public ADish Priced(decimal amount, DateTime validFrom)
        {
            return Priced(amount, validFrom, DateTime.MaxValue);
        }
        
        public ADish Priced(decimal amount, DateTime validFrom, DateTime validTo)
        {
            priceConfiguration.Add(new Price(amount, validFrom, validTo));
            return this;
        }

        public ADish OfType(string type)
        {
            this.type = type;
            return this;
        }

        public ADish WithReducedTax()
        {
            this.type = "Dish";
            return this;
        }

        public ADish WithRegularTax()
        {
            this.type = "Alcool";
            return this;
        }

        public ADish And()
        {
            return this;
        }

    }
}
