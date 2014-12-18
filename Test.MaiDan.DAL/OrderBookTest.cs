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
		
	}
}