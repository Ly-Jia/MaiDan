using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace MaiDan.Billing.Dal.Entities
{
    [Table("Dish")]
    public class Dish
    {
        /// <summary>
        /// Constructor used by Dapper
        /// </summary>
        public Dish()
        {
        } 
        
        public Dish(string id, List<Price> prices)
        {
            Id = id;
            Prices = prices;
        }

        [ExplicitKey]
        public string Id { get; set; }

        public List<Price> Prices { get; set; }

        public string Type { get; set; }
    }
}
