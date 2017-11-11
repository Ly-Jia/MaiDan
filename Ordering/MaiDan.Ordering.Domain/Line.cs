namespace MaiDan.Ordering.Domain
{
	public class Line
	{
	    public Line(int quantity, string dishId)
	    {
	        Quantity = quantity;
	        DishId = dishId;
	    }

	    public int Quantity { get; }
        public string DishId { get; }

	    public override bool Equals(object obj)
		{
			Line other = obj as Line;
			if (other == null)
				return false;
			return this.Quantity == other.Quantity && this.DishId == other.DishId;
		}

	}
}
