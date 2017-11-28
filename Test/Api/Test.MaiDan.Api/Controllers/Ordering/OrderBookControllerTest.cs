using System.Collections.Generic;
using System.Net;
using MaiDan.Api.Controllers.Ordering;
using MaiDan.Api.DataContract.Ordering;
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
        private Mock<IRepository<Table>> defaultRoom;
        private Table defaultTable = new Table("1");

        public OrderBookControllerTest()
        {
            defaultRoom = new Mock<IRepository<Table>>();
            defaultRoom.Setup(r => r.Get(defaultTable.Id)).Returns(defaultTable);
        }

        [Test]
        public void Http200WhenOrderIsFound()
        {
            var orderBook = new Mock<IRepository<Order>>();
            var order = new AnOrder().Build();
            orderBook.Setup(m => m.Get(It.IsAny<string>())).Returns(order);
            var orderBookController = CreateOrderBookController(orderBook.Object, null, defaultRoom.Object);

            var retrievedDish = orderBookController.Get("anId");

            Check.That(orderBookController.Response.StatusCode).Equals((int)HttpStatusCode.OK);
            Check.That(retrievedDish).Equals(order);
        }

        [Test]
        public void Http404WhenOrderIsNotFound()
        {
            var orderBook = new Mock<IRepository<Order>>();
            orderBook.Setup(m => m.Get(It.IsAny<string>())).Returns((Order)null);
            var orderBookController = CreateOrderBookController(orderBook.Object, null, defaultRoom.Object);

            orderBookController.Get("anId");

            Check.That(orderBookController.Response.StatusCode).Equals((int)HttpStatusCode.NotFound);
        }

        [Test]
        public void Http400WhenDishIsNotFoundDuringOrderCreation()
        {
            var orderBook = new Mock<IRepository<Order>>();
            var menu = new Mock<IRepository<Dish>>();
            menu.Setup(m => m.Get(It.IsAny<string>())).Returns((Dish)null);
            var orderBookController = CreateOrderBookController(orderBook.Object, menu.Object, defaultRoom.Object);

            orderBookController.Add(new OrderDataContract
            {
                Id = "id",
                Lines = new List<LineDataContract> {new LineDataContract {DishId = "id"}}
            });

            Check.That(orderBookController.Response.StatusCode).Equals((int)HttpStatusCode.BadRequest);
            orderBook.Verify(o => o.Add(It.IsAny<Order>()), Times.Never);
        }

        [Test]
        public void Http400WhenDishIsNotFoundDuringOrderUpdate()
        {
            var orderBook = new Mock<IRepository<Order>>();
            var menu = new Mock<IRepository<Dish>>();
            menu.Setup(m => m.Get(It.IsAny<string>())).Returns((Dish)null);
            var orderBookController = CreateOrderBookController(orderBook.Object, menu.Object, defaultRoom.Object);

            orderBookController.Update(new OrderDataContract
            {
                Id = "id",
                Lines = new List<LineDataContract> { new LineDataContract { DishId = "id" } }
            });

            Check.That(orderBookController.Response.StatusCode).Equals((int)HttpStatusCode.BadRequest);
            orderBook.Verify(o => o.Add(It.IsAny<Order>()), Times.Never);
        }

        [Test]
        public void CanAddOrderWithoutLines()
        {
            var tableId = "t1";
            var orderBook = new Mock<IRepository<Order>>();
            var menu = new Mock<IRepository<Dish>>();
            menu.Setup(m => m.Get(It.IsAny<string>())).Returns(new Dish("id", "name"));
            var room = new Mock<IRepository<Table>>();
            room.Setup(r => r.Get(tableId)).Returns(new Table(tableId));

            var orderBookController = CreateOrderBookController(orderBook.Object, menu.Object, room.Object);

            orderBookController.Add(new OrderDataContract { Id = "id", TableId = tableId });

            Check.That(orderBookController.Response.StatusCode).Equals((int)HttpStatusCode.OK);
        }

        [Test]
        public void CanUpdateOrderWithoutLines()
        {
            var tableId = "t1";
            var orderBook = new Mock<IRepository<Order>>();
            var menu = new Mock<IRepository<Dish>>();
            menu.Setup(m => m.Get(It.IsAny<string>())).Returns(new Dish("id", "name"));
            var room = new Mock<IRepository<Table>>();
            room.Setup(r => r.Get(tableId)).Returns(new Table(tableId));
            var orderBookController = CreateOrderBookController(orderBook.Object, menu.Object, room.Object);

            orderBookController.Update(new OrderDataContract { Id = "id", TableId = tableId });

            Check.That(orderBookController.Response.StatusCode).Equals((int)HttpStatusCode.OK);
        }

        private OrderBookController CreateOrderBookController(IRepository<Order> orderBook, IRepository<Dish> menu, IRepository<Table> room)
        {
            var orderBookController = new OrderBookController(orderBook, menu, room);
            orderBookController.ControllerContext = new ControllerContext();
            orderBookController.ControllerContext.HttpContext = new DefaultHttpContext();
            return orderBookController;
        }
    }
}
