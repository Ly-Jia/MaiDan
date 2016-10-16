using System;
using MaiDan.Service.Business.DataContract;
using MaiDan.Service.Domain;
using Moq;
using NUnit.Framework;
using Test.MaiDan.Infrastructure;

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
            VerifyContext.OutgoingResponseIsOK(chiefMock.Context);
        }

        [Test]
        public void should_not_update_a_missing_dish()
        {
            var aChief = new AChief().WithEmptyMenu();
            var chief = aChief.Build();
            var dishId = "anId";
            var newDishName = "aName";
            var dishDataContract = new DishDataContract { Id = dishId, Name = newDishName };
            
            chief.Update(dishDataContract);

            var expectedStatusDescription = String.Format("Cannot update dish {0} - {1} (does not exist)", dishId, newDishName);
            VerifyContext.OutgoingResponseIsKO(aChief.Context, expectedStatusDescription);
        }

        [Test]
        public void should_show_an_error_during_dish_creation_when_mandatory_fields_are_not_provided_in_data_contract()
        {
            var aChief = new AChief();
            var chief = aChief.Build();
            var dishDataContract = new Mock<IDataContract<Dish>>();
            dishDataContract.Setup(d => d.ToDomainObject()).Throws<ArgumentNullException>();
            
            chief.AddToMenu(dishDataContract.Object);
            
            VerifyContext.OutgoingResponseIsKOMissingMandatoryFields(aChief.Context);
        }
    }
}
