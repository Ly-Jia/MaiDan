using System.Net;
using MaiDan.Api.Controllers;
using MaiDan.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Api.Controllers.Ordering
{
    [TestFixture]
    public class MenuControllerTest
    {
        [Test]
        public void Http200WhenDishIsFound()
        {
            var menu = new Mock<IRepository<global::MaiDan.Billing.Domain.Dish>>();
            var id = "anId";
            var dish = new global::MaiDan.Billing.Domain.Dish(id, null);
            menu.Setup(m => m.Get(id)).Returns(dish);
            var menuController = CreateMenuController(null, menu.Object);

            var retrievedDish = menuController.Get(id);
             
            Check.That(menuController.Response.StatusCode).Equals((int)HttpStatusCode.OK);
            Check.That(retrievedDish).Equals(dish);
        }

        [Test]
        public void Http404WhenDishIsNotFound()
        {
            var menu = new Mock<IRepository<global::MaiDan.Billing.Domain.Dish>>();
            menu.Setup(m => m.Get(It.IsAny<string>())).Returns((global::MaiDan.Billing.Domain.Dish) null);
            var menuController = CreateMenuController(null, menu.Object);

            menuController.Get("anId");

            Check.That(menuController.Response.StatusCode).Equals((int)HttpStatusCode.NotFound);
        }
        
        private MenuController CreateMenuController(IRepository<global::MaiDan.Ordering.Domain.Dish> orderingMenu, IRepository<global::MaiDan.Billing.Domain.Dish> billingMenu)
        {
            var menuController = new MenuController(orderingMenu, billingMenu);
            menuController.ControllerContext = new ControllerContext();
            menuController.ControllerContext.HttpContext = new DefaultHttpContext();
            return menuController;
        }
    }
}
