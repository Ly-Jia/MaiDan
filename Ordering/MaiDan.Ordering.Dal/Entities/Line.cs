using Dapper.Contrib.Extensions;

namespace MaiDan.Ordering.Dal.Entities
{
    [Table("OrderLine")]
    public class Line
    {
        /// <summary>
        /// Constructor used by Dapper only
        /// </summary>
        public Line()
        { }

        public Line(int orderId, int quantity, Dish dish)
        {
            OrderId = orderId;
            Quantity = quantity;
            Dish = dish;
        }

        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public Dish Dish { get; set; }
    }
}
