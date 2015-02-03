using MaiDan.Business;
using MaiDan.DAL;
using MaiDan.Domain.Service;
using Moq;

namespace Test.MaiDan.Service
{
    public class AWaiter
    {
        private Mock<IRepository<Order>> OrderBook;

        public AWaiter()
        {
            OrderBook = new Mock<IRepository<Order>>();
        }

        public Waiter Build()
        {
            return new Waiter(OrderBook.Object);
        }
    }
}
