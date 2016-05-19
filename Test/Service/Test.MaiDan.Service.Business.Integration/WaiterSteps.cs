using System;
using System.Linq;
using MaiDan.Infrastructure;
using MaiDan.Infrastructure.Contract;
using MaiDan.Service.Business;
using MaiDan.Service.Dal;
using MaiDan.Service.Domain;
using NFluent;
using NHibernate.Linq;
using TechTalk.SpecFlow;

namespace Test.MaiDan.Service.Business.Integration
{
    [Binding]
    public class OrderBookSteps
    {
        private IRepository<Order, DateTime> orderBook;
        private IRepository<Dish, String> menu;
        private Order order;
        private Order retrievedOrder;
        private Waiter waiter;
        private readonly Database database = new Database();


        [Given(@"an order")]
        public void GivenAnOrder()
        {
            order = new AnOrder().Build();
        }

        [When(@"I take it")]
        public void WhenITakeIt()
        {
            waiter.Take(order);
        }

        [Then(@"I can keep it in my orderbook")]
        public void ICanKeepItInMyOrderbook()
        {
            using (var session = database.OpenSession())
            {
                var order = session.Query<Order>().Single();
                Check.That(order).Equals(this.order);
            }
        }

        [Given(@"an order in my orderbook")]
        public void GivenAnOrderInMyOrderbook()
        {
            GivenAnOrderInMyOrderbookWith(new Table("Quantity","DishId"));
        }

        [Given(@"an order in my orderbook with")]
        public void GivenAnOrderInMyOrderbookWith(Table lines)
        {
            var anOrder = new AnOrder();
            
            foreach (var line in lines.Rows)
            {
                anOrder.With(Convert.ToInt32(line["Quantity"]), line["DishId"]);
            }
            
            order = anOrder.Build();
            
            using (var session = database.OpenSession())
            {
                session.Transaction.Begin();
                session.Save(order);
                session.Transaction.Commit();
            }
        }

        [When(@"I search it")]
        public void WhenISearchIt()
        {
            orderBook = new OrderBook(database);
            retrievedOrder = orderBook.Get(order.Id);
        }

        [Then(@"I can consult the order's details")]
        public void ThenICanConsultTheOrderSDetails()
        {
            Check.That(retrievedOrder).Equals(order);
        }

        [When(@"I modify it with ([0-9]*) (.*)")]
        public void WhenIModifyItWithCoffee(int quantity, string dishName)
        {
            var orderToUpdate = new AnOrder(order.Id).With(quantity, dishName).Build();

            waiter.Update(orderToUpdate);
            order = orderToUpdate;
        }

        [Then(@"this order should be")]
        public void ThenThisOrderShouldBe(Table table)
        {
            using (var session = database.OpenSession())
            {
                var orderInOrderbook = session.Get<Order>(order.Id);
                Check.That(orderInOrderbook).Equals(order);
            }
        }

        [BeforeScenario("waiter")]
        public void deleteOrders()
        {
            using (var session = database.OpenSession())
            {
                session.Transaction.Begin();
                session.Delete("from Dish");
                session.Delete("from Line");
                session.Delete("from Order");
                session.Transaction.Commit();
            }

            orderBook = new OrderBook(database);
            menu = new Menu(database);
            menu.Add(new Dish("Coffee", "Coffee made with love"));
            waiter = new Waiter(orderBook, menu);
        }
    }
}
