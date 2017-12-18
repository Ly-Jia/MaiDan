namespace MaiDan.Ordering.Domain
{
	public class Line
	{
	    public Line(int quantity, Dish dish)
	    {
	        Quantity = quantity;
	        Dish = dish;
	    }

	    public int Quantity { get; }
        public Dish Dish { get; }

	    public override bool Equals(object obj)
		{
		    if (!(obj is Line other))
				return false;
            
			return this.Quantity == other.Quantity && this.Dish.Equals(other.Dish); // dish is not supposed to be null
		}

	}
}
