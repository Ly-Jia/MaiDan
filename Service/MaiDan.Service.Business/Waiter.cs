using System;
using System.ServiceModel;
using MaiDan.Infrastructure.Contract;
using MaiDan.Service.Business.DataContract;
using MaiDan.Service.Dal;
using MaiDan.Service.Domain;

namespace MaiDan.Service.Business
{
    [ServiceContract]
    public class Waiter
	{
		private IRepository<Order, DateTime> OrderBook;
	    private IRepository<Dish, String> Menu;

        public Waiter(IRepository<Order, DateTime> orderBook, IRepository<Dish, String> menu)
		{
			OrderBook = orderBook;
		    Menu = menu;
		}

        public Waiter() : this(new OrderBook(), new Menu()) { }

        [OperationContract]
		public void Take(OrderDataContract order)
		{
			OrderBook.Add(order.ToOrder());
		}

        [OperationContract]
	    public void Update(Order updatedOrder)
	    {
	        foreach (var line in updatedOrder.Lines)
	        {
	            if (!Menu.Contains(line.DishId))
	            {
	                throw new InvalidOperationException("Cannot add an unknown dish : " + line.DishId);
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

        [OperationContract]
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
