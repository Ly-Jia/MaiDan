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

        public Line(int orderId, int index, int quantity, bool free, Dish dish)
        {
            OrderId = orderId;
            Index = index;
            Quantity = quantity;
            Free = free;
            Dish = dish;
        }

        public int OrderId { get; set; }
        public int Index { get; set; }
        public int Quantity { get; set; }
        public bool Free { get; set; }
        [Required]
        public Dish Dish { get; set; }
    }
}
