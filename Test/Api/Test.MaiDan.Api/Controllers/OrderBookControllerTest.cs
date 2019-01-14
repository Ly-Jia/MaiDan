using MaiDan.Accounting.Domain;
using MaiDan.Api.Controllers;
using MaiDan.Api.Services;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NFluent;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Test.MaiDan.Billing;
using Test.MaiDan.Ordering;
using ADish = Test.MaiDan.Ordering.ADish;

namespace Test.MaiDan.Api.Controllers
{
    public class OrderBookControllerTest
    {
        private Table defaultTable;
        private DateTime defaultDay = DateTime.Today;
        private Mock<IRepository<Order>> orderBook;
        private Mock<IRepository<Dish>> menu;
        private Mock<IRepository<Table>> room;
        private Mock<ICashRegister> cashRegister;
        private Mock<ICalendar> calendar;

        [SetUp]
        public void Init()
        {
            defaultTable = new Table("1");
            orderBook = new Mock<IRepository<Order>>();
            menu = new Mock<IRepository<Dish>>();
            room = new Mock<IRepository<Table>>();
            room.Setup(r => r.Get(defaultTable.Id)).Returns(defaultTable);
            cashRegister = new Mock<ICashRegister>();
            calendar = new Mock<ICalendar>();
            calendar.Setup(c => c.GetCurrentDay()).Returns(new Day(defaultDay, false));
        }

        [Test]
        public void Http200WhenOrderIsFound()
        {
            var orderId = 1;
            var tcs = "tcs";
            var fiveEuros = 5m;
            var two = 2;
            var tacos = "tacos";
            var orderWithTwoTacos = new AnOrder(orderId).With(two, new ADish(tcs).Named(tacos).Build()).Build();
            orderBook.Setup(o => o.Get(orderId)).Returns(orderWithTwoTacos);

            var billForOrderWithTwoTacos = new ABill(orderId).With(fiveEuros * two).Build();
            cashRegister.Setup(cr => cr.Calculate(It.IsAny<Order>())).Returns(billForOrderWithTwoTacos);

            var orderBookController = CreateOrderBookController(orderBook.Object, null, room.Object, cashRegister.Object, calendar.Object);

            var result = orderBookController.Get(orderId).Result;

            Check.That(result).IsInstanceOf<OkObjectResult>();
            var okResult = (OkObjectResult)result;

            Check.That(okResult.Value).IsInstanceOf<global::MaiDan.Api.DataContracts.Responses.DetailedOrder>();
            var retrievedOrder = (global::MaiDan.Api.DataContracts.Responses.DetailedOrder)okResult.Value;

            Check.That(retrievedOrder.Id).Equals(orderWithTwoTacos.Id);
            Check.That(retrievedOrder.Lines.First().Quantity).Equals(two);
            Check.That(retrievedOrder.Lines.First().DishName).Equals(tacos);
            Check.That(retrievedOrder.Lines.First().Amount).Equals(2 * fiveEuros);
        }

        [Test]
        public void Http404WhenOrderIsNotFound()
        {
            orderBook.Setup(o => o.Get(It.IsAny<string>())).Returns((Order)null);
            var orderBookController = CreateOrderBookController(orderBook.Object, null, room.Object, cashRegister.Object, null);

            var result = orderBookController.Get(1).Result;

            Check.That(result).IsInstanceOf<NotFoundResult>();
        }

        [Test]
        public void Http500WhenPriceIsNotConfiguredForDetailedOrder()
        {
            orderBook.Setup(o => o.Get(It.IsAny<string>())).Returns((global::MaiDan.Ordering.Domain.Order)null);
            var dishId = "mistery";
            var dish = new Billing.ADish(dishId).Build();
            var order = new AnOrder().With(2, new ADish(dishId).Build()).Build();
            orderBook.Setup(o => o.Get(It.IsAny<object>())).Returns(order);
            cashRegister.Setup(cr => cr.Calculate(It.IsAny<Order>())).Throws(new InvalidOperationException());
            var orderBookController = CreateOrderBookController(orderBook.Object, null, room.Object, cashRegister.Object, null);

            Check.ThatCode(() => orderBookController.Get(1)).Throws<InvalidOperationException>();
        }

        [Test]
        public void Http400WhenDishIsNotFoundDuringOrderCreation()
        {
            menu.Setup(m => m.Get(It.IsAny<string>())).Returns((Dish)null);
            var orderBookController = CreateOrderBookController(orderBook.Object, menu.Object, room.Object, cashRegister.Object, calendar.Object);

            var result = orderBookController.Add(new global::MaiDan.Api.DataContracts.Requests.Order
            {
                Id = 1,
                Lines = new List<global::MaiDan.Api.DataContracts.Requests.Line> { new global::MaiDan.Api.DataContracts.Requests.Line { DishId = "id" } }
            });

            Check.That(result).IsInstanceOf<BadRequestObjectResult>();
            orderBook.Verify((IRepository<global::MaiDan.Ordering.Domain.Order> o) => o.Add(It.IsAny<global::MaiDan.Ordering.Domain.Order>()), Times.Never);
        }

        [Test]
        public void Http400WhenDishIsNotFoundDuringOrderUpdate()
        {
            orderBook.Setup(o => o.Get(1)).Returns(new AnOrder(1).With(1, new Dish("id", "Pork")).Build());
            menu.Setup(m => m.Get(It.IsAny<string>())).Returns((Dish)null);
            var orderBookController = CreateOrderBookController(orderBook.Object, menu.Object, room.Object, cashRegister.Object, calendar.Object);

            var result = orderBookController.Update(new global::MaiDan.Api.DataContracts.Requests.Order
            {
                Id = 1,
                Lines = new List<global::MaiDan.Api.DataContracts.Requests.Line> { new global::MaiDan.Api.DataContracts.Requests.Line { DishId = "id" } }
            });

            Check.That(result).IsInstanceOf<BadRequestObjectResult>();
            orderBook.Verify((IRepository<global::MaiDan.Ordering.Domain.Order> o) => o.Add(It.IsAny<global::MaiDan.Ordering.Domain.Order>()), Times.Never);
        }

        [Test]
        public void Http200WhenAddingOrderWithoutLines()
        {
            var tableId = "t1";
            orderBook.Setup(o => o.Add(It.IsAny<Order>())).Returns(1);
            menu.Setup(m => m.Get(It.IsAny<string>())).Returns(new Dish("id", "name"));
            room.Setup(r => r.Get(tableId)).Returns(new Table(tableId));

            var orderBookController = CreateOrderBookController(orderBook.Object, menu.Object, room.Object, cashRegister.Object, calendar.Object);

            var result = orderBookController.Add(new global::MaiDan.Api.DataContracts.Requests.Order { Id = 0, TableId = tableId, Lines = new List<global::MaiDan.Api.DataContracts.Requests.Line>(), NumberOfGuests = 2 });

            Check.That(result).IsInstanceOf<OkObjectResult>();
        }

        [Test]
        public void Http200WhenUpdatingOrderWithoutLines()
        {
            var tableId = "t1";
            orderBook.Setup(o => o.Get(1)).Returns(new OnSiteOrder(1, new Table(tableId), 2, defaultDay, new Line[0], false));
            menu.Setup(m => m.Get(It.IsAny<string>())).Returns(new Dish("id", "name"));
            room.Setup(r => r.Get(tableId)).Returns(new Table(tableId));
            var orderBookController = CreateOrderBookController(orderBook.Object, menu.Object, room.Object, cashRegister.Object, calendar.Object);

            var result = orderBookController.Update(new global::MaiDan.Api.DataContracts.Requests.Order
            {
                Id = 1,
                TableId = tableId,
                Lines = new List<global::MaiDan.Api.DataContracts.Requests.Line>(),
                NumberOfGuests = 2
            });

            Check.That(result).IsInstanceOf<OkResult>();
        }

        [Test]
        public void Http400WhenTryingToUpdateClosedOrder()
        {
            orderBook.Setup(o => o.Get(1)).Returns(new TakeAwayOrder(1, defaultDay, new[] { new Line(1, 1, false, new Dish("S1", "Spaghetti")) }, true));
            menu.Setup(m => m.Get(It.IsAny<string>())).Returns(new Dish("id", "name"));
            var orderBookController = CreateOrderBookController(orderBook.Object, menu.Object, room.Object, cashRegister.Object, calendar.Object);

            var result = orderBookController.Update(new global::MaiDan.Api.DataContracts.Requests.Order
            {
                Id = 1,
                TableId = null,
                Lines = new List<global::MaiDan.Api.DataContracts.Requests.Line>(),
                NumberOfGuests = 0
            });

            Check.That(result).IsInstanceOf<BadRequestObjectResult>();
        }

        private OrderBookController CreateOrderBookController(IRepository<global::MaiDan.Ordering.Domain.Order> orderBook, IRepository<Dish> menu, IRepository<Table> room, ICashRegister cashRegister, ICalendar calendar)
        {
            var orderBookController = new OrderBookController(orderBook, menu, room, cashRegister, calendar);
            orderBookController.ControllerContext = new ControllerContext();
            orderBookController.ControllerContext.HttpContext = new DefaultHttpContext();
            return orderBookController;
        }
    }
}
