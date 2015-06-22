using System;
using System.Collections.Generic;

namespace MaiDan.Domain.Service
{
	/// <summary>
	/// Description of Order.
	/// </summary>
	public class Order
	{
		public virtual DateTime Id { get; protected set; }
		public virtual IList<Line> Lines { get; protected set; }
		
		/// <summary>
		/// Constructor for NHibernate
		/// </summary>
        protected Order()
		{
			
		}
		
		public Order(DateTime creationDate, IList<Line> lines)
		{
			Id = creationDate;
			Lines = lines;
		}
		
		public virtual Order Add(Line line)
		{
			Lines.Add(line);
			return this;
		}
		
		public virtual Order Add(int quantity, String dishName)
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
