using MaiDan.Infrastructure;
using MaiDan.Service.Domain;
using Moq;
using NHibernate;

namespace Test.MaiDan.Infrastructure
{
    public class ADatabase
    {
        public Mock<ISession> Session;

        public ADatabase()
        {
            Session = new Mock<ISession>();   
        }

        public ADatabase With(Order order)
        {
            this.Session.Setup(s => s.Get<Order>(order.Id)).Returns(order);
            return this;
        }

        public IDatabase Build()
        {
            var transaction = new Mock<ITransaction>();
            Session.Setup(s => s.Transaction).Returns(transaction.Object);
            var database = new Mock<IDatabase>();
            database.Setup(d => d.OpenSession()).Returns(Session.Object);
            return database.Object;
        }
    }
}
