using System;
using System.Collections.Generic;
using MaiDan.Domain.Service;
using MaiDan.DAL;

namespace MaiDan.Business
{
	/// <summary>
	/// Description of Waiter.
	/// </summary>
	public class Waiter
	{
		public IRepository<Order> OrderBook;
		
		public Waiter(IRepository<Order> orderBook)
		{
			OrderBook = orderBook;
		}

		public void Take(Order order)
		{
			OrderBook.Add(order);
		}

	    public void Update(Order updatedOrder)
	    {
	        try
	        {
                OrderBook.Update(updatedOrder);
	        }
	        catch (ItemNotFoundException e)
	        {
	            throw e;
	        }
	        
	    }

	    public void AddDishToAnOrder(DateTime orderId, int quantity, string dishCode)
	    {
	        throw new ItemNotFoundException();
	    }
	}
	
	
}
