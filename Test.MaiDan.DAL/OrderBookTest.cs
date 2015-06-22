using System;
using MaiDan.DAL;
using MaiDan.Domain.Service;
using Moq;
using NFluent;
using NUnit.Framework;
using Test.MaiDan.Infrastructure;
using Test.MaiDan.Service;

namespace Test.MaiDan.DAL
{
	[TestFixture]
	public class OrderBookTest
	{
		[Test]
		public void can_add_order()
		{
		    var database = new ADatabase();
		    var orderBook = new OrderBook(database.Build());
			var order = new AnOrder().Build();
			
			orderBook.Add(order);
			
			database.Session.Verify(o => o.Save(order));
		}

	    [Test]
	    public void should_show_a_specific_order_from_the_id()
	    {
            var wantedOrder = new AnOrder(2012, 12, 21).Build();
            var database = new ADatabase().With(wantedOrder);
            var orderBook = new OrderBook(database.Build());

	        var retrievedOrder = orderBook.Get(new DateTime(2012, 12, 21));

	        Check.That(retrievedOrder).Equals(wantedOrder);
	    }

	    [Test]
	    public void should_show_error_when_order_is_not_found()
	    {
            var database = new ADatabase();
            database.Session.Setup(s => s.Get<Order>(It.IsAny<DateTime>())).Returns((Order)null);
            var orderBook = new OrderBook(database.Build());
           
            var orderId = new DateTime(2012, 12, 21);
            var exception = Assert.Throws<InvalidOperationException>(() => orderBook.Get(orderId));
	        Check.That(exception.Message).Equals("Order " + orderId + "was not found");
	    }

	    [Test]
	    public void can_update_lines_of_an_order()
	    {
            var database = new ADatabase();
            var orderBook = new OrderBook(database.Build());
            var order = new AnOrder().Build();
	        
            orderBook.Update(order);
           
            database.Session.Verify(s => s.Update(order));
	    }

        [Test]
        public void should_not_update_a_missing_order()
        {
            var database = new ADatabase();
            database.Session.Setup(s => s.Update(It.IsAny<Order>())).Throws<Exception>();
            var orderBook = new OrderBook(database.Build());
            var order = new AnOrder().Build();

            var exception = Assert.Throws<InvalidOperationException>(() => orderBook.Update(order));
            Check.That(exception.Message).Equals("Cannot update order : " + order.Id);
        }
	}
}