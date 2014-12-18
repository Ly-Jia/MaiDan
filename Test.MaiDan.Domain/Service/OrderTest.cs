using System;
using System.Collections.Generic;
using System.Linq;
using MaiDan.Domain.Service;
using NFluent;
using NUnit.Framework;
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
		
		[Test]
		public void should_add_directly_quantity_and_dish_to_an_order()
		{
			var order = new AnOrder().Build();
			var line = new Line(2, "Burgers");
			
			order = order.Add(2, "Burgers");
			var createdLine = order.Lines.First();
			
			Check.That(order.Lines).Contains(line);
		}
	}
}