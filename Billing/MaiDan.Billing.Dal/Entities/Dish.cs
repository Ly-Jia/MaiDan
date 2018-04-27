using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Billing.Dal.Entities
{
    [Table("BillDish")]
    public class Dish
    {
        public Dish()
        {
        }

        public Dish(string id, List<Price> prices, string type)
        {
            Id = id;
            Prices = prices;
            Type = type;
        }

        public string Id { get; set; }

        public List<Price> Prices { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
