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
		private IRepository<Order> OrderBook;
	    private IList<String> Menu; 
		
		public Waiter(IRepository<Order> orderBook, IList<String> menu)
		{
			OrderBook = orderBook;
		    Menu = menu;
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
	        try
	        {
                var order = OrderBook.Get(orderId);
                if (!Menu.Contains(dishCode))
                {
                    throw new ItemNotFoundException();
                }
                order.Add(quantity, dishCode);
                OrderBook.Update(order);
	        }
            catch (ItemNotFoundException e)
	        {
	            throw e;
	        }
	    }
	}
	
	
}
