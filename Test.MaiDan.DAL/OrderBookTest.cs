using System;
using System.Collections;
using System.Collections.Generic;
using MaiDan.DAL;
using MaiDan.Domain.Service;
using Moq;
using Test.MaiDan.Service;
using NUnit.Framework;
using NFluent;

namespace Test.MaiDan.DAL
{
	[TestFixture]
	public class OrderBookTest
	{
		[Test]
		public void can_add_order()
		{
		    var orders = new Mock<IList<Order>>();
			var orderBook = new OrderBook(orders.Object);
			var order = new AnOrder().Build();
			
			orderBook.Add(order);
			
			orders.Verify(o => o.Add(order));
		}

	    [Test]
	    public void should_show_a_specific_order_from_the_id()
	    {
            var wantedOrder = new AnOrder(2012, 12, 21).Build();
	        var orderBook = new OrderBook(new List<Order> {wantedOrder});

	        var retrievedOrder = orderBook.Get(new DateTime(2012, 12, 21));

	        Check.That(retrievedOrder).Equals(wantedOrder);
	    }

	    [Test]
	    public void should_show_error_when_order_is_not_found()
	    {
            var orderBook = new OrderBook(new List<Order>());
	        var orderId = new DateTime(2012, 12, 21);

            var exception = Assert.Throws<InvalidOperationException>(() => orderBook.Get(orderId));
	        Check.That(exception.Message).Equals("Order " + orderId + "was not found");
	    }

	    [Test]
	    /// <summary>
	    /// a(n ugly) test to ensure that we modify the right order
	    /// when deleting it, remember to delete virtual attribute added to
	    /// "Order" class constructor and its "Update" function 
	    /// They were necessary for mocking.
	    /// </summary>
	    public void can_update_lines_of_an_order()
	    {
	        var orderBookMock = new Mock<OrderBook>(){CallBase = true};
            var order = new AnOrder().With(1, "Cheese Nan").Build();
            var orderToUpdate = new Mock<Order>();

            orderBookMock.Setup(ob => ob.Get(order.Id)).Returns(orderToUpdate.Object);
	        
            orderBookMock.Object.Update(order);
           
            //verify update sur la commande
            orderToUpdate.Verify(o => o.Update(order.Lines));
	    }

        [Test]
        /// <summary>
        /// a(nother ugly) test to ensure that update fails when order is not found
        /// when deleting it, remember to delete virtual attribute added to
        /// "Order" class constructor and its "Update" function 
        /// They were necessary for mocking.
        /// </summary>
        public void should_not_update_a_missing_order()
        {
            var orderBookMock = new Mock<OrderBook>() { CallBase = true };
            var order = new AnOrder().With(1, "Cheese Nan").Build();

            orderBookMock.Setup(ob => ob.Get(order.Id)).Throws(new InvalidOperationException());

            //verify update sur la commande
            var exception = Assert.Throws<InvalidOperationException>(() => orderBookMock.Object.Update(order));
            Check.That(exception.Message).Equals("Cannot update order : " + order.Id);
        }
	}
}