using System.Collections.Generic;
using System.Linq;
using MaiDan.DAL;
using MaiDan.Domain.Service;
using MaiDan.Infrastructure;
using NFluent;
using NHibernate.Linq;
using TechTalk.SpecFlow;
using Test.MaiDan.Service;

namespace Test.MaiDan.DAL.Integration
{
    [Binding]
    public class OrderBookSteps
    {
        private OrderBook _orderBook;
        private Order _order;
        private Order _retrievedOrder;
        private readonly Database _database = new Database();

        [Given(@"an order")]
        public void GivenAnOrder()
        {
            _orderBook = new OrderBook(_database);
            _order = new AnOrder().Build();
        }

        [When(@"I take it")]
        public void WhenITakeIt()
        {
            _orderBook.Add(_order);
        }

        [Then(@"I can keep it in my orderbook")]
        public void ICanKeepItInMyOrderbook()
        {
            using (var session = _database.OpenSession())
            {
                var order = session.Query<Order>().Single();
                Check.That(order).Equals(_order);
            }
        }

        [Given(@"an order in my orderbook")]
        public void GivenAnOrderInMyOrderbook()
        {
            _order = new AnOrder().Build();

            using (var session = _database.OpenSession())
            {
                session.Transaction.Begin();
                session.Save(_order);
                session.Transaction.Commit();
            }
        }

        [When(@"I search it")]
        public void WhenISearchIt()
        {
            _orderBook = new OrderBook(_database);
            _retrievedOrder = _orderBook.Get(_order.Id);
        }

        [Then(@"I can consult the order's details")]
        public void ThenICanConsultTheOrderSDetails()
        {
            Check.That(_retrievedOrder).Equals(_order);
        }

        [BeforeScenario()]
        public void deleteOrders()
        {
            using (var session = _database.OpenSession())
            {
                session.Transaction.Begin();
                session.Delete("from Line");
                session.Delete("from Order");
                session.Transaction.Commit();
            }
        }
    }
}
