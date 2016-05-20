using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MaiDan.Infrastructure.Contract;
using MaiDan.Service.Business;
using MaiDan.Service.Domain;
using Moq;
using NUnit.Framework;

namespace Test.MaiDan.Service.Business
{
    [TestFixture]
    public class ChiefTest
    {
        [Test]
        public void can_update_a_dish()
        {
            var menuMock = new Mock<IRepository<Dish, string>>();
            var chief = new Chief(menuMock.Object);

            var dishId = "anId";
            var newDishName = "aName";
            var dish = new Dish(dishId, newDishName);

            chief.Update(dishId,newDishName);
            menuMock.Verify(menu => menu.Update(dish),Times.Once);
        }
    }
}
