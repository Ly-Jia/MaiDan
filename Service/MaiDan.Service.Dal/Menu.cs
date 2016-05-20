using System;
using MaiDan.Infrastructure;
using MaiDan.Infrastructure.Contract;
using MaiDan.Service.Domain;

namespace MaiDan.Service.Dal
{
    public class Menu : IRepository<Dish, String>
    {
        private IDatabase database;

        public Menu(IDatabase _database)
        {
            database = _database;
        }

        public Menu() : this(new Database()) { }

        public Dish Get(string id)
        {
            throw new NotImplementedException();
        }

        public void Add(Dish dish)
        {
            using (var session = database.OpenSession())
            {
                session.Transaction.Begin();
                session.Save(dish);
                session.Transaction.Commit();
            }
        }

        public void Update(Dish dish)
        {
            using (var session = database.OpenSession())
            {
                session.Transaction.Begin();
                session.Update(dish);
                session.Transaction.Commit();
            }
        }
        
        public bool Contains(String id)
        {
            Dish dish;
            using (var session = database.OpenSession())
            {
                dish = session.Get<Dish>(id);
            }

            return dish != null;
        }
    }
}
