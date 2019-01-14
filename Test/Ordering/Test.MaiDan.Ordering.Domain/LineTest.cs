using MaiDan.Ordering.Domain;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Ordering.Domain
{
	public class LineTest
	{
		[Test]
		public void should_be_equal_when_id_and_quantity_and_dish_are_the_same()
		{
		    var dish = new Dish("T","Tomato");
		    var line1 = new Line(1, 1, false, dish);
			var line2 = new Line(1, 1, false, dish);
			
			var isEqual = line1.Equals(line2);
			
			Check.That(isEqual).IsTrue();
		}
		
		[Test]
		public void should_not_be_equal_when_dish_is_not_the_same()
		{
			var line1 = new Line(1, 1, false, new Dish("T","Tomato"));
			var line2 = new Line(1, 1, false, new Dish("P","Potato"));
			
			var isEqual = line1.Equals(line2);
			
			Check.That(isEqual).IsFalse();
		}

	    [Test]
	    public void should_not_be_equal_when_quantity_is_not_the_same()
	    {
	        var dish = new Dish("T", "Tomato");
	        var line1 = new Line(1, 1, false, dish);
	        var line2 = new Line(1, 2, false, dish);

	        var isEqual = line1.Equals(line2);

	        Check.That(isEqual).IsFalse();
        }

        [Test]
        public void should_not_be_equal_when_id_is_not_the_same()
        {
            var dish = new Dish("T", "Tomato");
            var line1 = new Line(1, 1, false, dish);
            var line2 = new Line(2, 1, false, dish);

            var isEqual = line1.Equals(line2);

            Check.That(isEqual).IsFalse();
        }
    }
}
