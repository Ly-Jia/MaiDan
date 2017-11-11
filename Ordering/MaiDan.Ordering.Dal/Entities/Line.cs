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

        public Line(int orderId, int quantity, string dishId)
        {
            OrderId = orderId;
            Quantity = quantity;
            DishId = dishId;
        }

        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public string DishId { get; set; }
    }
}
