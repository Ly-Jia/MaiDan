using System;
using System.Collections.Generic;
using System.Net;
using MaiDan.Service.Business.DataContract;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Service.Business
{
	[TestFixture]
	public class WaiterTest
	{
		[Test]
		public void can_take_an_order_from_a_date()
		{
		    var aWaiter = new AWaiter();
		    var waiter = aWaiter.Build();

            var orderDataContract = new OrderDataContract() { Id = AnOrder.DEFAULT_ID };
            var order = new AnOrder().Build();
			
			waiter.Take(orderDataContract);

            aWaiter.OrderBook.Verify(OrderBook => OrderBook.Add(order));
            
            aWaiter.Context.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusCode = HttpStatusCode.OK);
        }

        [Test]
        public void should_not_add_a_dish_to_a_missing_order()
	    {
            var waiter = new AWaiter().WithoutOrder().Build();
            var orderId = new DateTime();

            var exception = Assert.Throws<InvalidOperationException>(() => waiter.AddDishToAnOrder(orderId, 2, "Fried rice"));
            
            Check.That(exception.Message).Equals("Cannot add a dish to an order: " + orderId);
	    }

	    [Test]
	    public void should_add_a_dish_to_an_order()
	    {
	        var aMenu = new List<String> {"Coffee", "Donut"};

	        var existingOrder = new AnOrder().With(1, "Coffee").Build();
	        var waiterMock = new AWaiter().Handing(aMenu).With(existingOrder);

	        var waiter = waiterMock.Build();

            waiter.AddDishToAnOrder(existingOrder.Id, 2, "Donut");

	        var updatedOrder = new AnOrder(existingOrder.Id).With(1, "Coffee").And(2, "Donut").Build();

	        waiterMock.OrderBook.Verify(ob => ob.Update(updatedOrder));
	    }


	    [Test]
	    public void can_update_an_order()
	    {
            var waiterMock = new AWaiter();
	        var waiter = waiterMock.Build();

	        var updatedOrder = new AnOrder().Build();
            waiter.Update(updatedOrder);

            waiterMock.OrderBook.Verify(o => o.Update(updatedOrder));
	    }

	    [Test]
	    public void should_not_update_a_missing_order()
	    {
	        var aWaiter = new AWaiter().WithoutOrder();
	        var waiter = aWaiter.Build();
	        var order = new AnOrder().Build();
	        var statusDescription = "Cannot update order: " + order.Id;

            waiter.Update(order);
            
            aWaiter.Context.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusCode = HttpStatusCode.InternalServerError);
            aWaiter.Context.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusDescription = statusDescription);
	    }

	   
	    [Test]
	    public void should_not_update_an_order_with_a_missing_dish()
	    {
	        var order = new AnOrder().Build();
	        var aWaiter = new AWaiter().With(order);
	        var waiter = aWaiter.Build();
	        var dishCode = "Unknown";
	        var orderToUpdate = new AnOrder(order.Id).With(1, dishCode).Build();
            var statusDescription = "Cannot add an unknown dish: "+ dishCode;

	        waiter.Update(orderToUpdate);

            aWaiter.Context.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusCode = HttpStatusCode.InternalServerError);
	        aWaiter.Context.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusDescription = statusDescription);
	    }
	}
}