using System;
using System.Linq;
using MaiDan.Infrastructure;
using MaiDan.Service.Dal;
using MaiDan.Service.Domain;
using NFluent;
using NHibernate.Linq;
using TechTalk.SpecFlow;

namespace Test.MaiDan.Service.Dal.Integration
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
            GivenAnOrderInMyOrderbookWith(new Table("Quantity","Dish"));
        }

        [Given(@"an order in my orderbook with")]
        public void GivenAnOrderInMyOrderbookWith(Table lines)
        {
            var anOrder = new AnOrder();

            foreach (var line in lines.Rows)
            {
                anOrder.With(Convert.ToInt32(line["Quantity"]), line["Dish"]);
            }
            
            _order = anOrder.Build();
            
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

        [When(@"I modify it with ([0-9]*) (.*)")]
        public void WhenIModifyItWithCoffee(int quantity, string dishName)
        {
            var orderToUpdate = new AnOrder(_order.Id).With(quantity, dishName).Build();

            _orderBook = new OrderBook(_database);
            _orderBook.Update(orderToUpdate);
            _order = orderToUpdate;
        }

        [Then(@"this order should be")]
        public void ThenThisOrderShouldBe(Table table)
        {
            using (var session = _database.OpenSession())
            {
                var orderInOrderbook = session.Get<Order>(_order.Id);
                Check.That(orderInOrderbook).Equals(_order);
            }
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
