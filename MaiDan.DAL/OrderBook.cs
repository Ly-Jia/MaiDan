using System;
using System.Collections.Generic;
using System.Linq;
using MaiDan.Domain.Service;

namespace MaiDan.DAL
{
	/// <summary>
	/// Description of OrderBook.
	/// </summary>
	public class OrderBook : IRepository<Order>
	{
		public IList<Order> Orders;
		
		public OrderBook()
		{
			Orders = new List<Order>();
		}
		
		public void Add(Order order)
		{
			Orders.Add(order);
		}

	    public object Get(DateTime orderId)
	    {
	        return Orders.Single(o => o.Id == orderId);
	    }
	}
}
