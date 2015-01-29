using System;
using System.Collections.Generic;

namespace MaiDan.Domain.Service
{
	/// <summary>
	/// Description of Order.
	/// </summary>
	public class Order
	{
		public DateTime Id;
		public IList<Line> Lines;
		
		/// <summary>
		/// Constructor for test purpose only (mock in OrderBookTest)
		/// </summary>
		public Order():this(new DateTime())
		{
			
		}
		
		public Order(DateTime creationDate)
		{
			Id = creationDate;
			Lines = new List<Line>();
		}
		
		public Order Add(Line line)
		{
			Lines.Add(line);
			return this;
		}
		
		public Order Add(int quantity, String dishName)
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

		/// <remarks>
		/// virtual attribute was added for testing purpose only
		/// (mock in OrderBookTest)
		/// </remarks>
		/// <param name="updatedLines">new values</param>
		public virtual void Update(IList<Line> updatedLines)
		{
			Lines = updatedLines;
		}
	}
}
