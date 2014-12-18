using System;
using System.Collections.Generic;
using MaiDan.DAL;
using MaiDan.Domain.Service;
using NUnit.Framework;
using NFluent;

namespace Test.MaiDan.DAL
{
	[TestFixture]
	public class OrderBookTest
	{
		[Test]
		public void should_add_order()
		{
			var orderBook = new OrderBook();
			var order = new Order(new DateTime(2012,12,21));
			
			orderBook.Add(order);
			
			Check.That(orderBook.Orders).Contains(order);
		}
		
	}
}