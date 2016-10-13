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
            try
            {
                OrderBook.Add(order.ToOrder());
            }
            catch (Exception)
            {
                var statusDescription = String.Format("Order n°{0} could not be taken", order.Id);
                SetContextOutgoingResponseStatusToKO(statusDescription);
            }

            SetContextOutgoingResponseStatusToOK();
        }
        
        [OperationContract]
	    public void Update(Order updatedOrder)
	    {
	        foreach (var line in updatedOrder.Lines)
	        {
	            if (!Menu.Contains(line.DishId))
	            {
	                var statusDescription = String.Format("Cannot add an unknown dish: {0}",line.DishId);
	                SetContextOutgoingResponseStatusToKO(statusDescription);
	            }
	        }
	        try
	        {
                OrderBook.Update(updatedOrder);
	        }
	        catch (Exception)
	        {
	            var statusDescription = String.Format("Cannot update order: {0}", updatedOrder.Id);
	            SetContextOutgoingResponseStatusToKO(statusDescription);
	        }

            SetContextOutgoingResponseStatusToOK();
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

        private void SetContextOutgoingResponseStatusToOK()
        {
            context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
        }
        private void SetContextOutgoingResponseStatusToKO(string statusDescription)
        {
            context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            context.OutgoingResponse.StatusDescription = statusDescription;
        }
    }
	
	
}
