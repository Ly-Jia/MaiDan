using System;
using System.Collections.Generic;
using System.Linq;
using MaiDan.Domain.Service;

namespace MaiDan.DAL
{
	/// <summary>
	/// Description of OrderBook.
	/// </summary>
	public class OrderBook : IRepository<Order, DateTime>
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

	    public virtual Order Get(DateTime orderId)
	    {
	        var order = Orders.SingleOrDefault(o => o.Id == orderId);
	        if (order == null)
	        {
	            throw new ItemNotFoundException();
	        }
	        return order;
	    }

	    public void Update(Order item)
	    {
	    	var orderToUpdate = this.Get(item.Id);
	    	orderToUpdate.Update(item.Lines);
	    }

	    public bool Contains(DateTime id)
	    {
	        throw new NotImplementedException();
	    }
	}
}
