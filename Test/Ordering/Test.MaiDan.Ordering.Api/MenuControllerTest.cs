using System.Net;
using MaiDan.Ordering.Api.Controllers;
using MaiDan.Ordering.Dal;
using MaiDan.Ordering.Domain;
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
            var menu = new Mock<Menu>();
            menu.Setup(m => m.Get(It.IsAny<string>())).Returns((Dish)null);
            var menuController = new MenuController(menu.Object);

            menuController.Get("anId");

            Check.That(menuController.Response.StatusCode).Equals(HttpStatusCode.NotFound);
        }

        [Test]
        public void Http404WhenDishIsNotFound()
        {
            var menu = new Mock<Menu>();
            menu.Setup(m => m.Get(It.IsAny<string>())).Returns((Dish) null);
            var menuController = new MenuController(menu.Object);

            menuController.Get("anId");

            Check.That(menuController.Response.StatusCode).Equals(HttpStatusCode.NotFound);
        }
    }
}
