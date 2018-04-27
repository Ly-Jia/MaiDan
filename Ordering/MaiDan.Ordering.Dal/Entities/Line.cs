using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Ordering.Dal.Entities
{
    [Table("OrderLine")]
    public class Line
    {
        public Line()
        {
        }

        public Line(int orderId, int index, int quantity, Dish dish)
        {
            OrderId = orderId;
            Index = index;
            Quantity = quantity;
            Dish = dish;
        }

        public int OrderId { get; set; }
        public int Index { get; set; }
        public int Quantity { get; set; }
        [Required]
        public Dish Dish { get; set; }
    }
}
