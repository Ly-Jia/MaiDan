using System;
using System.Collections.Generic;
using MaiDan.Business;
using MaiDan.DAL;
using MaiDan.Domain.Service;
using Moq;

namespace Test.MaiDan.Service
{
    public class AWaiter
    {
        public Mock<IRepository<Order>> OrderBook;
        private IList<string> Menu; 

        public AWaiter()
        {
            OrderBook = new Mock<IRepository<Order>>();
            Menu = new List<string>();
        }

        public AWaiter With(Order order)
        {
            OrderBook.Setup(ob => ob.Get(order.Id)).Returns(order);
            return this;
        }

        public AWaiter WithoutOrder()
        {
            OrderBook.Setup(ob => ob.Get(It.IsAny<DateTime>())).Throws<ItemNotFoundException>();
            OrderBook.Setup(ob => ob.Update(It.IsAny<Order>())).Throws<ItemNotFoundException>();
            return this;
        }

        public AWaiter Handing(IList<String> menu)
        {
            Menu = menu;
            return this;
        }

        public Waiter Build()
        {
            return new Waiter(OrderBook.Object, Menu);
        }
    }
}
