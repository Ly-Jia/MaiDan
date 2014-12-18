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
	}
}
