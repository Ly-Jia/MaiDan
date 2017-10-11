using System.Net;
using MaiDan.Api.Controllers.Ordering;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NFluent;
using NUnit.Framework;
using Test.MaiDan.Ordering;

namespace Test.MaiDan.Api.Controllers.Ordering
{
    public class OrderBookControllerTest
    {
        [Test]
        public void Http200WhenDishIsFound()
        {
            var orderBook = new Mock<IRepository<Order>>();
            var order = new AnOrder().Build();
            orderBook.Setup(m => m.Get(It.IsAny<string>())).Returns(order);
            var menuController = CreateOrderBookController(orderBook.Object);

            var retrievedDish = menuController.Get("anId");

            Check.That(menuController.Response.StatusCode).Equals((int)HttpStatusCode.OK);
            Check.That(retrievedDish).Equals(order);
        }

        [Test]
        public void Http404WhenDishIsNotFound()
        {
            var orderBook = new Mock<IRepository<Order>>();
            orderBook.Setup(m => m.Get(It.IsAny<string>())).Returns((Order)null);
            var menuController = CreateOrderBookController(orderBook.Object);

            menuController.Get("anId");

            Check.That(menuController.Response.StatusCode).Equals((int)HttpStatusCode.NotFound);
        }

        private OrderBookController CreateOrderBookController(IRepository<Order> orderBook)
        {
            var orderBookController = new OrderBookController(orderBook);
            orderBookController.ControllerContext = new ControllerContext();
            orderBookController.ControllerContext.HttpContext = new DefaultHttpContext();
            return orderBookController;
        }
    }
}
