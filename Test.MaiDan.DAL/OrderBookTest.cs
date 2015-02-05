using System;
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
			var orderBook = new OrderBook();
			var order = new AnOrder().Build();
			
			orderBook.Add(order);
			
			Check.That(orderBook.Orders).Contains(order);
		}

	    [Test]
	    public void should_show_a_specific_order_from_the_id()
	    {
            var wantedOrder = new AnOrder(2012, 12, 21).Build();
	        var orderBook = new OrderBook() {Orders = new List<Order> {wantedOrder}};

	        var retrievedOrder = orderBook.Get(new DateTime(2012, 12, 21));

	        Check.That(retrievedOrder).Equals(wantedOrder);
	    }

	    [Test]
	    public void should_show_error_when_order_is_not_found()
	    {
            var orderBook = new OrderBook();

            Assert.Throws<ItemNotFoundException>(() => orderBook.Get(new DateTime(2012, 12, 21)));
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
	}
}