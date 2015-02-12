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
