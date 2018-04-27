using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Ordering.Dal.Entities
{
    [Table("OrderDish")]
    public class Dish 
    {
        public Dish(string id, string name)
        {
            Id = id;
            Name = name;
        }
        
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}
