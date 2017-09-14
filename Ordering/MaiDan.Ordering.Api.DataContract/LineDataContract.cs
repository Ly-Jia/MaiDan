using MaiDan.Ordering.Domain;

namespace MaiDan.Ordering.DataContract
{
    public class LineDataContract
    {
        public int Id { get; set; }
        
        public int Quantity { get; set; }
        
        public string DishId { get; set; }

        public Line ToLine()
        {
            return new Line(this.Quantity, this.DishId);
        }
    }
}
