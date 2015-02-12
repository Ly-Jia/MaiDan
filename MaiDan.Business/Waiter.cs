using System;
using System.Collections.Generic;
using System.Linq;
using MaiDan.Domain.Service;
using MaiDan.DAL;

namespace MaiDan.Business
{
	/// <summary>
	/// Description of Waiter.
	/// </summary>
	public class Waiter
	{
		private IRepository<Order, DateTime> OrderBook;
	    private IRepository<Dish, String> Menu;

        public Waiter(IRepository<Order, DateTime> orderBook, IRepository<Dish, String> menu)
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
	        if (updatedOrder.Lines.Any(line => !Menu.Contains(line.DishCode)))
	        {
	            throw new ItemNotFoundException();
	        }
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
                order.Add(quantity, dishCode);
                this.Update(order);
	        }
            catch (ItemNotFoundException e)
	        {
	            throw e;
	        }
	    }
	}
	
	
}
