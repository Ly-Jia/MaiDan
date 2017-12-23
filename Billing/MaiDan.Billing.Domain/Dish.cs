using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Billing.Domain
{
    public class Dish
    {
        public Dish(string id, List<Price> priceConfiguration)
        {
            Id = id;
            PriceConfiguration = priceConfiguration;
        }

        public string Id { get; }

        public List<Price> PriceConfiguration { get; }

        public decimal CurrentPrice => PriceConfiguration.Single(p => p.ValidityEndDate.Equals(DateTime.MinValue)).Amount;

        public override bool Equals(object obj)
        {
            if (!(obj is Dish other))
                return false;
            return other.Id == this.Id && other.PriceConfiguration.SequenceEqual(this.PriceConfiguration);
        }
    }
}
