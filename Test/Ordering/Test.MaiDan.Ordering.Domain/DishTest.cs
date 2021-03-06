﻿using MaiDan.Ordering.Domain;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Ordering.Domain
{
    public class DishTest
    {
        [Test]
        public void should_be_equal_when_id_and_name_and_type_the_same()
        {
            var dish1 = new Dish("id", "dishName");
            var dish2 = new Dish("id", "dishName");

            var isEqual = dish1.Equals(dish2);

            Check.That(isEqual).IsTrue();
        }

        [Test]
        public void should_not_be_equal_when_id_is_not_the_same()
        {
            var dish1 = new Dish("id1", "dishName");
            var dish2 = new Dish("id2", "dishName");

            var isEqual = dish1.Equals(dish2);

            Check.That(isEqual).IsFalse();
        }

        [Test]
        public void should_not_be_equal_when_name_is_not_the_same()
        {
            var dish1 = new Dish("id", "dishName1");
            var dish2 = new Dish("id", "dishName2");

            var isEqual = dish1.Equals(dish2);

            Check.That(isEqual).IsFalse();
        }
    }
}
