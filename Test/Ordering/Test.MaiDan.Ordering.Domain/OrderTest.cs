using System;
using System.Collections.Generic;
using System.Linq;
using MaiDan.Ordering.Domain;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Ordering.Domain
{
	[TestFixture]
	public class OrderTest
	{
		[Test]
		public void should_be_identifiable_by_id()
		{
			var id = "id";
			var order = new Order(id, new List<Line>());
			Check.That(order.Id).Equals(id);
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
			var order1 = new AnOrder("1").Build();
			var order2 = new AnOrder("1").Build();
			
			var isEqual = order1.Equals(order2);
			
			Check.That(isEqual).IsTrue();
		}
		
		[Test]
		public void should_not_be_equal_when_id_is_not_the_same()
		{
			var order1 = new AnOrder("id1").Build();
			var order2 = new AnOrder("id2").Build();
			
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