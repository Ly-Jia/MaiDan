using System.Collections.Generic;

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
    }
}
