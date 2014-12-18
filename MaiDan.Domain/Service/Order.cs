using System;

namespace MaiDan.Domain.Service
{
	/// <summary>
	/// Description of Order.
	/// </summary>
	public class Order
	{
		public DateTime Id;
		public Order(DateTime creationDate)
		{
			Id = creationDate;
		}
	}
}
