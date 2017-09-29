using System;
using System.Collections.Generic;

namespace MaiDan.Ordering.Domain
{
	/// <summary>
	/// Description of Order.
	/// </summary>
	public class Order
	{
		public virtual string Id { get; protected set; }
		public virtual IList<Line> Lines { get; protected set; }
		
		/// <summary>
		/// Constructor for Dapper
		/// </summary>
        protected Order()
		{
			
		}
		
		public Order(string id, IList<Line> lines)
		{
			Id = id;
			Lines = lines;
		}
		
		public virtual Order Add(Line line)
		{
			Lines.Add(line);
			return this;
		}
		
		public virtual Order Add(int quantity, string dishName)
		{
			return this.Add(new Line(quantity, dishName));
		}
		
		public override bool Equals(object obj)
		{
			Order other = obj as Order;
			if (other == null)
				return false;
			return this.Id == other.Id;
		}

		public virtual void Update(IList<Line> updatedLines)
		{
			Lines = updatedLines;
		}
	}
}
