namespace MaiDan.Ordering.Domain
{
	public class Line
	{
		public virtual int Quantity { get; protected set; }
		public virtual string DishId { get; protected set; }
        
        /// <summary>
        /// constructor only used by Dapper
        /// </summary>
	    protected Line()
	    {
	        
	    }

		public Line(int quantity, string dishId)
		{
			Quantity = quantity;
			DishId = dishId;
		}
		
		public override bool Equals(object obj)
		{
			Line other = obj as Line;
			if (other == null)
				return false;
			return this.Quantity == other.Quantity && this.DishId == other.DishId;
		}

	}
}
