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

        public Line(int orderId, int index, int quantity, Dish dish)
        {
            Id = $"{orderId}-{index}";
            OrderId = orderId;
            Index = index;
            Quantity = quantity;
            Dish = dish;
        }

        [ExplicitKey]
        public string Id { get; set; }
        public int OrderId { get; set; }
        public int Index { get; set; }
        public int Quantity { get; set; }
        public Dish Dish { get; set; }
    }
}
