using System;
using System.Collections;
using System.Collections.Generic;
using MaiDan.Business;
using MaiDan.DAL;
using MaiDan.Domain.Service;
using Moq;

namespace Test.MaiDan.Service
{
    public class AWaiter
    {
        private Mock<IRepository<Order>> orderBook;

        public AWaiter()
        {
            orderBook = new Mock<IRepository<Order>>();
        }

        public AWaiter With(Order order)
        {
            orderBook.Setup(ob => ob.Get(order.Id)).Returns(order);
            return this;
        }

        public AWaiter WithoutOrder()
        {
            orderBook.Setup(ob => ob.Get(It.IsAny<DateTime>())).Throws<ItemNotFoundException>();
            orderBook.Setup(ob => ob.Update(It.IsAny<Order>())).Throws<ItemNotFoundException>();
            return this;
        }

        public Waiter Build()
        {
            return new Waiter(orderBook.Object);
        }
    }
}
