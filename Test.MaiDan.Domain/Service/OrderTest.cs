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
		public void can_add_line_to_an_order()
		{
			var order = new AnOrder().Build();
			var line = new Line(2, "Burgers");
			
			order = order.Add(line);
			
			Check.That(order.Lines).Contains(line);
		}
		
		[Test]
		public void can_add_directly_quantity_and_dish_to_an_order()
		{
			var order = new AnOrder().Build();
			var line = new Line(2, "Burgers");
			
			order = order.Add(2, "Burgers");
			var createdLine = order.Lines.First();
			
			Check.That(order.Lines).Contains(line);
		}
		
		[Test] 
		public void should_be_equal_when_id_is_the_same()
		{
			var order1 = new AnOrder(2012,12,21).Build();
			var order2 = new AnOrder(2012,12,21).Build();
			
			var isEqual = order1.Equals(order2);
			
			Check.That(isEqual).IsTrue();
		}
		
		[Test]
		public void should_not_be_equal_when_id_is_not_the_same()
		{
			var order1 = new AnOrder(2012,12,12).Build();
			var order2 = new AnOrder(2011,11,11).Build();
			
			var isEqual = order1.Equals(order2);
			
			Check.That(isEqual).IsFalse();
		}
		
		[Test]
		public void can_update_lines()
		{
			var initialLine = new Line(1, "Taco");
			var order = new AnOrder().With(initialLine).Build();
			
			var updatedLine = new Line(1, "Burrito");
			order.Update(new List<Line> {updatedLine});
				
			Check.That(order.Lines).Contains(updatedLine);
			Check.That(order.Lines).Not.Contains(initialLine);
		}
	}
}