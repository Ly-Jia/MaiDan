using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using MaiDan.Infrastructure.Business;
using MaiDan.Infrastructure.Contract;
using MaiDan.Service.Business.DataContract;
using MaiDan.Service.Dal;
using MaiDan.Service.Domain;

namespace MaiDan.Service.Business
{
    [ServiceContract]
    public class Waiter
	{
        private IWebOperationContext context;
		private IRepository<Order, DateTime> OrderBook;
	    private IRepository<Dish, String> Menu;

        public Waiter(IWebOperationContext context, IRepository<Order, DateTime> orderBook, IRepository<Dish, String> menu)
		{
			OrderBook = orderBook;
		    Menu = menu;
            this.context = context;
		}

        public Waiter() : this(new WebOperationContextWrapper(WebOperationContext.Current),new OrderBook(), new Menu()) { }

        [OperationContract]
		public void Take(OrderDataContract order)
		{
			OrderBook.Add(order.ToOrder());
            context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
        }

        [OperationContract]
	    public void Update(Order updatedOrder)
	    {
	        foreach (var line in updatedOrder.Lines)
	        {
	            if (!Menu.Contains(line.DishId))
	            {
                    context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    context.OutgoingResponse.StatusDescription = String.Format("Cannot add an unknown dish: {0}",line.DishId);
	            }
	        }
	        try
	        {
                OrderBook.Update(updatedOrder);
	        }
	        catch (Exception e)
	        {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                context.OutgoingResponse.StatusDescription = String.Format("Cannot update order: {0}", updatedOrder.Id);
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
                throw new InvalidOperationException(String.Format("Cannot add a dish to an order: {0}",orderId), e);
	        }
	    }
	}
	
	
}
