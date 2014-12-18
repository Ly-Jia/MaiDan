using System;
using System.Collections.Generic;
using NUnit.Framework;
using NFluent;
using MaiDan.Domain.Service;
using Test.MaiDan.Service;

namespace Test.MaiDan.Domain.Service
{
	[TestFixture]
	public class OrderTest
	{
		[Test]
		public void should_be_identifiable_by_the_creation_date()
		{
			var creationDate = new DateTime(2012,12,21);
			var order = new Order(creationDate);
			Check.That(order.Id).Equals(creationDate);
		}
		
		[Test]
		public void should_add_line_to_an_order()
		{
			var order = new AnOrder().Build();
			var line = new Line(2, "Burgers");
			
			order = order.Add(line);
			
			Check.That(order.Lines).Contains(line);
		}
		
	}
}