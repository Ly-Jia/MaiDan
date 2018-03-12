using MaiDan.Ordering.Domain;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Ordering.Domain
{
	[TestFixture]
	public class LineTest
	{
		[Test]
		public void should_be_equal_when_id_and_quantity_and_dish_are_the_same()
		{
		    var dish = new Dish("T","Tomato", "Fruit");
		    var line1 = new Line(1, 1, dish);
			var line2 = new Line(1, 1, dish);
			
			var isEqual = line1.Equals(line2);
			
			Check.That(isEqual).IsTrue();
		}
		
		[Test]
		public void should_not_be_equal_when_dish_is_not_the_same()
		{
			var line1 = new Line(1, 1, new Dish("T","Tomato", "Fruit"));
			var line2 = new Line(1, 1, new Dish("P","Potato", "Vegetable"));
			
			var isEqual = line1.Equals(line2);
			
			Check.That(isEqual).IsFalse();
		}

	    [Test]
	    public void should_not_be_equal_when_quantity_is_not_the_same()
	    {
	        var dish = new Dish("T", "Tomato", "Fruit");
	        var line1 = new Line(1, 1, dish);
	        var line2 = new Line(1, 2, dish);

	        var isEqual = line1.Equals(line2);

	        Check.That(isEqual).IsFalse();
        }

        [Test]
        public void should_not_be_equal_when_id_is_not_the_same()
        {
            var dish = new Dish("T", "Tomato", "Fruit");
            var line1 = new Line(1, 1, dish);
            var line2 = new Line(2, 1, dish);

            var isEqual = line1.Equals(line2);

            Check.That(isEqual).IsFalse();
        }
    }
}
