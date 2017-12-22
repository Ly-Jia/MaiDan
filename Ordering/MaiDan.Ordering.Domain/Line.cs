namespace MaiDan.Ordering.Domain
{
	public class Line
	{
	    public Line(int id, int quantity, Dish dish)
	    {
            Id = id;
	        Quantity = quantity;
	        Dish = dish;
	    }

        public int Id { get; }
	    public int Quantity { get; }
        public Dish Dish { get; }

	    public override bool Equals(object obj)
		{
		    if (!(obj is Line other))
				return false;
            
			return this.Id == other.Id && this.Quantity == other.Quantity && this.Dish.Equals(other.Dish); // dish is not supposed to be null
		}

	}
}
