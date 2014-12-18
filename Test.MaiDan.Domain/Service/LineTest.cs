using System;
using MaiDan.Domain.Service;
using NUnit.Framework;
using NFluent;

namespace Test.MaiDan.Domain.Service
{
	[TestFixture]
	public class LineTest
	{
		[Test]
		public void should_be_equal_when_quantity_and_dish_name_are_the_same()
		{
			var line1 = new Line(1, "Tomato");
			var line2 = new Line(1, "Tomato");
			
			var isEqual = line1.Equals(line2);
			
			Check.That(isEqual).IsTrue();
		}
		
		[Test]
		public void should_not_be_equal_when_quantity_and_dish_name_are_not_the_same()
		{
			var line1 = new Line(1, "Tomato");
			var line2 = new Line(1, "Potato");
			
			var isEqual = line1.Equals(line2);
			
			Check.That(isEqual).IsFalse();
		}
	}
}
