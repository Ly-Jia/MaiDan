using System;
using System.Collections.Generic;
using MaiDan.DAL;
using MaiDan.Domain.Service;
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
	    public void can_show_a_specific_order_from_the_id()
	    {
            var wantedOrder = new AnOrder(2012, 12, 21).Build();
	        var orderBook = new OrderBook() {Orders = new List<Order> {wantedOrder}};

	        var retrievedOrder = orderBook.Get(new DateTime(2012, 12, 21));

	        Check.That(retrievedOrder).Equals(wantedOrder);
	    }

	}
}