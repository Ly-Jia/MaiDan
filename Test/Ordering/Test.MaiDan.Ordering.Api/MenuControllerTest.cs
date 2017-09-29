using System;
using System.Net;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Api.Controllers;
using MaiDan.Ordering.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Ordering.Api
{
    [TestFixture]
    public class MenuControllerTest
    {
        [Test]
        public void Http200WhenDishIsFound()
        {
            var menu = new Mock<IRepository<Dish>>();
            var dish = new ADish().Build();
            menu.Setup(m => m.Get(It.IsAny<string>())).Returns(dish);
            var menuController = CreateMenuController(menu.Object);

            var retrievedDish = menuController.Get("anId");

            Check.That(menuController.Response.StatusCode).Equals((int)HttpStatusCode.OK);
            Check.That(retrievedDish).Equals(dish);
        }

        [Test]
        public void Http404WhenDishIsNotFound()
        {
            var menu = new Mock<IRepository<Dish>>();
            menu.Setup(m => m.Get(It.IsAny<string>())).Returns((Dish) null);
            var menuController = CreateMenuController(menu.Object);

            menuController.Get("anId");

            Check.That(menuController.Response.StatusCode).Equals((int)HttpStatusCode.NotFound);
        }
        
        private MenuController CreateMenuController(IRepository<Dish> menu)
        {
            var menuController = new MenuController(menu);
            menuController.ControllerContext = new ControllerContext();
            menuController.ControllerContext.HttpContext = new DefaultHttpContext();
            return menuController;
        }
    }
}
