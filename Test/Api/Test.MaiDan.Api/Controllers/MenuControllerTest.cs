﻿using MaiDan.Api.Controllers;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Api.Controllers
{
    public class MenuControllerTest
    {
        private IRepository<Dish> emptyOrderingMenu = new Mock<IRepository<global::MaiDan.Ordering.Domain.Dish>>().Object;

        [Test]
        public void Http200WhenDishIsFound()
        {
            var orderingMenu = new Mock<IRepository<global::MaiDan.Ordering.Domain.Dish>>();
            var billingMenu = new Mock<IRepository<global::MaiDan.Billing.Domain.Dish>>();
            var id = "anId";
            var name = "name";
            var orderingDish = new global::MaiDan.Ordering.Domain.Dish(id, name);
            orderingMenu.Setup(m => m.Get(id)).Returns(orderingDish);
            var billingDish = new global::MaiDan.Billing.Domain.Dish(id, null, null);
            billingMenu.Setup(m => m.Get(id)).Returns(billingDish);
            var menuController = CreateMenuController(orderingMenu.Object, billingMenu.Object);

            var result = menuController.Get(id).Result;

            Check.That(result).IsInstanceOf<OkObjectResult>();
            var okResult = (OkObjectResult)result;

            Check.That(okResult.Value).IsInstanceOf<global::MaiDan.Api.DataContracts.Responses.DetailedDish>();
            var retrievedDish = (global::MaiDan.Api.DataContracts.Responses.DetailedDish)okResult.Value;

            Check.That(retrievedDish.Id).Equals(id);
            Check.That(retrievedDish.Name).Equals(name);
        }

        [Test]
        public void Http404WhenDishIsNotFound()
        {
            var menu = new Mock<IRepository<global::MaiDan.Billing.Domain.Dish>>();
            menu.Setup(m => m.Get(It.IsAny<string>())).Returns((global::MaiDan.Billing.Domain.Dish) null);
            var menuController = CreateMenuController(emptyOrderingMenu, menu.Object);

            var result = menuController.Get("anId");

            Check.That(result.Result).IsInstanceOf<NotFoundResult>();
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
