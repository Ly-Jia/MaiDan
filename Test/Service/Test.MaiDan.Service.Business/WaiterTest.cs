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
		    var waiterMock = new AWaiter();
		    var waiter = waiterMock.Build();
            var orderDataContract = new OrderDataContract() { Id = AnOrder.DEFAULT_ID };
            
			waiter.Take(orderDataContract);

            var order = new AnOrder().Build();
            waiterMock.OrderBook.Verify(OrderBook => OrderBook.Add(order));
            waiterMock.Context.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusCode = HttpStatusCode.OK);
        }

	    [Test]
	    public void should_notify_when_taking_order_failed()
	    {
            var waiterWithFailingOrderBookMock = new AWaiter().WithFailingOrderBook();
            var waiterWithFailingOrderBook = waiterWithFailingOrderBookMock.Build();
            var orderDataContract = new OrderDataContract() { Id = AnOrder.DEFAULT_ID };
            
            waiterWithFailingOrderBook.Take(orderDataContract);

            var statusDescription = String.Format("Order n°{0} could not be taken", orderDataContract.Id);
            waiterWithFailingOrderBookMock.Context.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusCode = HttpStatusCode.InternalServerError);
            waiterWithFailingOrderBookMock.Context.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusDescription = statusDescription);
        }

        [Test]
        public void should_not_add_a_dish_to_a_missing_order()
	    {
            var waiterWithoutOrderMock = new AWaiter().WithoutOrder();
            var waiterWithoutOrder = waiterWithoutOrderMock.Build();
            var orderId = AnOrder.DEFAULT_ID;
            
            waiterWithoutOrder.AddDishToAnOrder(orderId, 2, "Fried rice");

            var statusDescription = String.Format("Cannot add a dish to an order: {0}", orderId);
            waiterWithoutOrderMock.Context.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusCode = HttpStatusCode.InternalServerError);
            waiterWithoutOrderMock.Context.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusDescription = statusDescription);
	    }

	    [Test]
	    public void should_add_a_dish_to_an_order()
	    {
	        var aMenu = new List<String> {"Coffee", "Donut"};
	        var existingOrder = new AnOrder().With(1, "Coffee").Build();
	        var waiterWithMenuAndOrderMock = new AWaiter().Handing(aMenu).With(existingOrder);
	        var waiterWithMenuAndOrder = waiterWithMenuAndOrderMock.Build();

            waiterWithMenuAndOrder.AddDishToAnOrder(existingOrder.Id, 2, "Donut");

	        var updatedOrder = new AnOrder(existingOrder.Id).With(1, "Coffee").And(2, "Donut").Build();
	        waiterWithMenuAndOrderMock.OrderBook.Verify(ob => ob.Update(updatedOrder));
	    }


	    [Test]
	    public void can_update_an_order()
	    {
            var waiterMock = new AWaiter();
	        var waiter = waiterMock.Build();
	        var updatedOrder = new AnOrder().Build();

            waiter.Update(updatedOrder);

            waiterMock.OrderBook.Verify(o => o.Update(updatedOrder));
            waiterMock.Context.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusCode = HttpStatusCode.OK);
        }

	    [Test]
	    public void should_not_update_a_missing_order()
	    {
	        var waiterWithoutOrderMock = new AWaiter().WithoutOrder();
	        var waiterWithoutOrder = waiterWithoutOrderMock.Build();
	        var order = new AnOrder().Build();

            waiterWithoutOrder.Update(order);

            var statusDescription = "Cannot update order: " + order.Id;
            waiterWithoutOrderMock.Context.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusCode = HttpStatusCode.InternalServerError);
            waiterWithoutOrderMock.Context.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusDescription = statusDescription);
	    }

	   
	    [Test]
	    public void should_not_update_an_order_with_a_missing_dish()
	    {
	        var order = new AnOrder().Build();
	        var waiterWithOrderMock = new AWaiter().With(order);
	        var waiterWithOrder = waiterWithOrderMock.Build();
	        var unknownDishCode = "Unknown";
	        var orderToUpdate = new AnOrder(order.Id).With(1, unknownDishCode).Build();
            
	        waiterWithOrder.Update(orderToUpdate);

            var statusDescription = "Cannot add an unknown dish: " + unknownDishCode;
            waiterWithOrderMock.Context.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusCode = HttpStatusCode.InternalServerError);
	        waiterWithOrderMock.Context.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusDescription = statusDescription);
	    }
	}
}