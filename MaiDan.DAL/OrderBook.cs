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
		private IList<Order> orders;

        /// <summary>
        /// Constructor only for test in OrderBookTest
        /// </summary>
	    public OrderBook()
	    {
	        
	    }

		public OrderBook(IList<Order> _orders)
		{
            orders = _orders;
		}
		
		public void Add(Order order)
		{
			orders.Add(order);
		}

	    public virtual Order Get(DateTime orderId)
	    {
	        var order = orders.SingleOrDefault(o => o.Id == orderId);
	        if (order == null)
	        {
                throw new InvalidOperationException("Order " + orderId + "was not found");
	        }
	        return order;
	    }

	    public void Update(Order item)
	    {
	        try
	        {
	            var orderToUpdate = Get(item.Id);
	            orderToUpdate.Update(item.Lines);
	        }
	        catch (Exception e)
	        {
                throw new InvalidOperationException("Cannot update order : " + item.Id, e);
	        }
	    }

	    public bool Contains(DateTime id)
	    {
	        throw new NotImplementedException();
	    }
	}
}
