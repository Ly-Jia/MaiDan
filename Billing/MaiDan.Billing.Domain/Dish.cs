using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Billing.Domain
{
    public class Dish
    {
        public Dish(string id, List<Price> priceConfiguration, string type)
        {
            Id = id;
            PriceConfiguration = priceConfiguration ?? new List<Price>();
            Type = type;
        }

        public string Id { get; }

        public List<Price> PriceConfiguration { get; }

        public string Type { get; }

        public decimal? CurrentPrice => PriceConfiguration.Count == 0 ? (decimal?) null : PriceConfiguration.Single(p => p.ValidityEndDate.Equals(DateTime.MinValue)).Amount;

        public override bool Equals(object obj)
        {
            if (!(obj is Dish other))
                return false;
            return other.Id == this.Id && other.PriceConfiguration.SequenceEqual(this.PriceConfiguration) && other.Type.Equals(this.Type);
        }
    }
}
