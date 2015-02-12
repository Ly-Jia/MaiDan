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
	        foreach (var line in updatedOrder.Lines)
	        {
	            if (!Menu.Contains(line.DishCode))
	            {
	                throw new InvalidOperationException("Cannot add an unknown dish : " + line.DishCode);
	            }
	        }
	        try
	        {
                OrderBook.Update(updatedOrder);
	        }
	        catch (Exception e)
	        {
                throw new InvalidOperationException("Cannot update order : " + updatedOrder.Id, e);
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
            catch (Exception e)
	        {
                throw new InvalidOperationException("Cannot add a dish to an order : " + orderId, e);
	        }
	    }
	}
	
	
}
