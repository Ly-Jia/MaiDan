using System;
using System.Collections.Generic;
using MaiDan.Infrastructure.Contract;
using MaiDan.Service.Business;
using MaiDan.Service.Domain;
using Moq;
using Test.MaiDan.Infrastructure;

namespace Test.MaiDan.Service
{
    public class AWaiter
    {
        public Mock<IRepository<Order,DateTime>> OrderBook { get; private set; }
        private Mock<IRepository<Dish, String>> Menu;
        public AWebOperationContext Context;

        public AWaiter()
        {
            OrderBook = new Mock<IRepository<Order,DateTime>>();
            Menu = new Mock<IRepository<Dish, String>>();
            Context = new AWebOperationContext();
        }

        public AWaiter With(Order order)
        {
            OrderBook.Setup(ob => ob.Get(order.Id)).Returns(order);
            return this;
        }

        public AWaiter WithoutOrder()
        {
            OrderBook.Setup(ob => ob.Get(It.IsAny<DateTime>())).Throws<InvalidOperationException>();
            OrderBook.Setup(ob => ob.Update(It.IsAny<Order>())).Throws<InvalidOperationException>();
            return this;
        }

        public AWaiter Handing(IList<String> menu)
        {
            foreach (var dish in menu)
            {
                Menu.Setup(m => m.Contains(dish)).Returns(true);
            }
            return this;
        }

        public Waiter Build()
        {
            return new Waiter(Context.Object, OrderBook.Object, Menu.Object);
        }
    }
}
