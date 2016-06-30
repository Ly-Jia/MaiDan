using System;
using MaiDan.Service.Business.DataContract;
using MaiDan.Service.Domain;
using Moq;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Service.Business
{
    [TestFixture]
    public class ChiefTest
    {
        [Test]
        public void can_update_a_dish()
        {
            var chiefMock = new AChief();
            var chief = chiefMock.Build();

            var dishId = "anId";
            var newDishName = "aName";
            var dish = new Dish(dishId, newDishName);
            var dishContract = new DishDataContract { Id = dishId, Name = newDishName };

            chief.Update(dishContract);

            chiefMock.Menu.Verify(menu => menu.Update(dish),Times.Once);
        }

        [Test]
        public void should_not_update_a_missing_dish()
        {
            var chief = new AChief().WithEmptyMenu().Build();

            var dishId = "anId";
            var newDishName = "aName";
            var dishContract = new DishDataContract { Id = dishId, Name = newDishName };
            var exception = Assert.Throws<InvalidOperationException>(() => chief.Update(dishContract));

            Check.That(exception.Message).Equals($"Cannot update dish {dishId} - {newDishName} (does not exist)");
        }
    }
}
