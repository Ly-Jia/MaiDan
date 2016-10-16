using System;
using NUnit.Framework;
using Test.MaiDan.Infrastructure;

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
            var orderDataContract = new AnOrder().ToOrderContract();

            waiter.Take(orderDataContract);

            var order = new AnOrder().Build();
            waiterMock.OrderBook.Verify(OrderBook => OrderBook.Add(order));
		    VerifyContext.OutgoingResponseIsOK(waiterMock.Context);
        }

	    [Test]
	    public void should_notify_when_taking_order_failed()
	    {
            var waiterWithFailingOrderBookMock = new AWaiter().WithFailingOrderBook();
            var waiterWithFailingOrderBook = waiterWithFailingOrderBookMock.Build();
            var orderDataContract = new AnOrder().ToOrderContract();
            
            waiterWithFailingOrderBook.Take(orderDataContract);

            var expectedStatusDescription = String.Format("Order n°{0} could not be taken", orderDataContract.Id);
	        VerifyContext.OutgoingResponseIsKO(waiterWithFailingOrderBookMock.Context, expectedStatusDescription);
	    }
        
	    [Test]
	    public void can_update_an_order()
	    {
            var waiterMock = new AWaiter();
	        var waiter = waiterMock.Build();
	        var updatedOrder = new AnOrder().ToOrderContract();

            waiter.Update(updatedOrder);

            waiterMock.OrderBook.Verify(o => o.Update(updatedOrder.ToOrder()));
            VerifyContext.OutgoingResponseIsOK(waiterMock.Context);
        }

	    [Test]
	    public void should_not_update_a_missing_order()
	    {
	        var waiterWithoutOrderMock = new AWaiter().WithoutOrder();
	        var waiterWithoutOrder = waiterWithoutOrderMock.Build();
	        var order = new AnOrder().ToOrderContract();

            waiterWithoutOrder.Update(order);

            var expectedStatusDescription = String.Format("Cannot update order: {0}",order.Id);
            VerifyContext.OutgoingResponseIsKO(waiterWithoutOrderMock.Context, expectedStatusDescription);
        }

	   
	    [Test]
	    public void should_not_update_an_order_with_a_missing_dish()
	    {
	        var order = new AnOrder().Build();
	        var waiterWithOrderMock = new AWaiter().With(order);
	        var waiterWithOrder = waiterWithOrderMock.Build();
	        var unknownDishCode = "Unknown";
	        var orderToUpdate = new AnOrder(order.Id).With(1, unknownDishCode).ToOrderContract();

            waiterWithOrder.Update(orderToUpdate);

            var expectedStatusDescription = String.Format("Cannot add an unknown dish: {0}", unknownDishCode);
            VerifyContext.OutgoingResponseIsKO(waiterWithOrderMock.Context, expectedStatusDescription);
        }
	}
}