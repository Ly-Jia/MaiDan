using System;

namespace MaiDan.Domain.Service
{
	public class Line
	{
        /// <summary>
        /// Id used only for NHibernate
        /// </summary>
        public virtual int Id { get; protected set; } 
		public virtual int Quantity { get; protected set; }
		public virtual String DishId { get; protected set; }
        
        /// <summary>
        /// constructor only used by NHibernate
        /// </summary>
	    protected Line()
	    {
	        
	    }

		public Line(int quantity, String dishId)
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
