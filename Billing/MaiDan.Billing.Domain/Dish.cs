using System.Collections.Generic;

namespace MaiDan.Billing.Domain
{
    public class Dish
    {
        private List<Price> priceConfiguration;

        public Dish(string id, List<Price> priceConfiguration)
        {
            Id = id;
            this.priceConfiguration = priceConfiguration;
        }

        public string Id { get; private set; }
        
        public decimal Price { get; }
    }
}
