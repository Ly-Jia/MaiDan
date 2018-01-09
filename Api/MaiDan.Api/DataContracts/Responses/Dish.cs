using Newtonsoft.Json;

namespace MaiDan.Api.DataContracts.Responses
{
    public class Dish 
    {
        public Dish(Ordering.Domain.Dish orderingDish, Billing.Domain.Dish billingDish)
        {
            Id = orderingDish.Id;
            Name = orderingDish.Name;
            Price = billingDish.CurrentPrice;
        }

        public string Id { get; set; }
        
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; }
    }
}
