using System;
using System.Collections.Generic;
using MaiDan.Business;
using MaiDan.DAL;
using MaiDan.Domain.Service;
using Moq;
using NFluent;
using NUnit.Framework;
using Test.MaiDan.Service;

namespace Test.MaiDan.Business
{
	[TestFixture]
	public class WaiterTest
	{
		[Test]
		public void can_take_an_order_from_a_date()
		{
		    var waiterMock = new AWaiter();
		    var waiter = waiterMock.Build();

			var order = new AnOrder().Build();
			
			waiter.Take(order);
			
			waiterMock.OrderBook.Verify(OrderBook => OrderBook.Add(order));
		}

        [Test]
        public void should_not_add_a_dish_to_a_missing_order()
	    {
            var waiter = new AWaiter().WithoutOrder().Build();
            var orderId = new DateTime();

            var exception = Assert.Throws<InvalidOperationException>(() => waiter.AddDishToAnOrder(orderId, 2, "Fried rice"));
            
            Check.That(exception.Message).Equals("Cannot add a dish to an order : " + orderId);
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
	        var waiter = new AWaiter().WithoutOrder().Build();
	        var order = new AnOrder().Build();

	        var exception = Assert.Throws<InvalidOperationException>(() => waiter.Update(order));

	        Check.That(exception.Message).Equals("Cannot update order : " + order.Id);
	    }

	   
	    [Test]
	    public void should_not_update_an_order_with_a_missing_dish()
	    {
	        var order = new AnOrder().Build();
	        var waiter = new AWaiter().With(order).Build();
	        var dishCode = "Unknown";
	        var orderToUpdate = new AnOrder(order.Id).With(1, dishCode).Build();

            var exception = Assert.Throws<InvalidOperationException>(() => waiter.Update(orderToUpdate));

            Check.That(exception.Message).Equals("Cannot add an unknown dish : " + dishCode);
	    }
	}
}