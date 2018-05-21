using MaiDan.Api.Controllers;
using MaiDan.Api.DataContracts.Requests;
using MaiDan.Api.Services;
using MaiDan.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NFluent;
using NUnit.Framework;
using System.Net;
using Test.MaiDan.Ordering;

namespace Test.MaiDan.Api.Controllers
{
    [TestFixture]
    public class BillBookControllerTest
    {
        [Test]
        public void Http200WhenBillIsPrintedOnce()
        {
            var id = 8;
            var billBook = new Mock<IRepository<global::MaiDan.Billing.Domain.Bill>>();
            var orderBook = new Mock<IRepository<global::MaiDan.Ordering.Domain.Order>>();
            orderBook.Setup(o => o.Get(id)).Returns(new AnOrder(id).Build());
            var cashRegister = new Mock<ICashRegister>();

            var billBookController = CreateBillBookController(billBook.Object, orderBook.Object, cashRegister.Object);
            var contract = new Order { Id = id };

            billBookController.Print(contract);

            Check.That(billBookController.Response.StatusCode).Equals((int)HttpStatusCode.OK);
            orderBook.Verify(o => o.Get(id), Times.Once());
            cashRegister.Verify(cr => cr.Print(It.IsAny<global::MaiDan.Ordering.Domain.Order>()), Times.Once());
        }

        [Test]
        public void Http400WhenBillIsPrintedTwice()
        {
            var id = 2;
            var billBook = new Mock<IRepository<global::MaiDan.Billing.Domain.Bill>>();
            var orderBook = new Mock<IRepository<global::MaiDan.Ordering.Domain.Order>>();
            orderBook.Setup(o => o.Get(id)).Returns(new AnOrder(id).AlreadyPrinted().Build());
            var cashRegister = new Mock<ICashRegister>();

            var billBookController = CreateBillBookController(billBook.Object, orderBook.Object, cashRegister.Object);
            var contract = new Order { Id = id };

            billBookController.Print(contract);

            Check.That(billBookController.Response.StatusCode).Equals((int)HttpStatusCode.BadRequest);
            orderBook.Verify(o => o.Get(id), Times.Once());
            cashRegister.Verify(cr => cr.Print(It.IsAny<global::MaiDan.Ordering.Domain.Order>()), Times.Never());
        }

        private BillBookController CreateBillBookController(IRepository<global::MaiDan.Billing.Domain.Bill> billBook, IRepository<global::MaiDan.Ordering.Domain.Order> orderBook, ICashRegister cashRegister)
        {
            var billBookController = new BillBookController(billBook, orderBook, cashRegister);
            billBookController.ControllerContext = new ControllerContext();
            billBookController.ControllerContext.HttpContext = new DefaultHttpContext();
            return billBookController;
        }
    }
}
