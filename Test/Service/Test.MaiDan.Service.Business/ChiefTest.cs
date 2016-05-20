using System;
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

            chief.Update(dishId,newDishName);

            chiefMock.Menu.Verify(menu => menu.Update(dish),Times.Once);
        }

        [Test]
        public void should_not_update_a_missing_dish()
        {
            var chief = new AChief().WithEmptyMenu().Build();

            var dishId = "anId";
            var newDishName = "aName";
            var exception = Assert.Throws<InvalidOperationException>(() => chief.Update(dishId, newDishName));

            Check.That(exception.Message).Equals($"Cannot update dish {dishId} - {newDishName} (does not exist)");
        }
    }
}
