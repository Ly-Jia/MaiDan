using System;
using System.Collections.Generic;
using MaiDan.Business;
using MaiDan.Domain.Service;
using MaiDan.DAL;
using Test.MaiDan.Service;
using NUnit.Framework;
using NFluent;
using Moq;

namespace Test.MaiDan.Business
{
	[TestFixture]
	public class WaiterTest
	{
		[Test]
		public void can_take_an_order_from_a_date()
		{
			var orderBook = new Mock<IRepository<Order>>();
			var waiter = new Waiter(orderBook.Object);
			var order = new AnOrder().Build();
			
			waiter.Take(order);
			
			orderBook.Verify(OrderBook => OrderBook.Add(order));
		}
		
		[Test]
		public void should_modify_an_order()
		{	
		}
		
		[Test]
		public void should_delete_an_order()
		{
			
		}
		
	}
}