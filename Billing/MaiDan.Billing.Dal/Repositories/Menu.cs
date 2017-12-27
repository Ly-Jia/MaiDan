using System.Collections.Generic;
using System.Linq;
using Dapper;
using MaiDan.Billing.Dal.Entities;
using MaiDan.Infrastructure.Database;
using Z.Dapper.Plus;

namespace MaiDan.Billing.Dal.Repositories
{
    public class Menu : IRepository<Domain.Dish>
    {
        private IDatabase database;

        public Menu(IDatabase database)
        {
            this.database = database;
        }

        public Domain.Dish Get(string id)
        {
            string sql = "SELECT price.DishId as Id, price.DishId, price.ValidityStartDate, price.ValidityEndDate, price.Amount " +
                         "FROM \"DishPrice\" price " +
                         $"WHERE price.DishId = '{id}' ;";

            using (var connection = database.CreateConnection())
            {
                connection.Open();

                var dishDictionary = new Dictionary<string, Dish>();
                
                var dish = connection.Query<Dish, Price, Dish>(
                        sql,
                        (d, p) =>
                        {
                            if (!dishDictionary.TryGetValue(d.Id, out Dish dishEntry))
                            {
                                dishEntry = d;
                                dishEntry.Prices = new List<Price>();
                                dishDictionary.Add(dishEntry.Id, dishEntry);
                            }

                            dishEntry.Prices.Add(p);
                            return dishEntry;
                        },
                        splitOn: "DishId")
                    .Distinct()
                    .Single();

                return ModelFrom(dish);
            }
        }

        public List<Domain.Dish> GetAll()
        {
            const string sql = "SELECT price.DishId as Id, price.* FROM \"DishPrice\" price ;";

            using (var connection = database.CreateConnection())
            {
                connection.Open();

                var dishDictionary = new Dictionary<string, Dish>();

                var dishes = connection.Query<Dish, Price, Dish>(
                        sql,
                        (d, p) =>
                        {
                            if (!dishDictionary.TryGetValue(d.Id, out var dishEntry))
                            {
                                dishEntry = d;
                                dishEntry.Prices = new List<Price>();
                                dishDictionary.Add(dishEntry.Id, dishEntry);
                            }

                            dishEntry.Prices.Add(p);
                            return dishEntry;
                        },
                        splitOn: "DishId")
                    .Distinct()
                    .ToList();

                return dishes.Select(ModelFrom).ToList();
            }
        }

        public void Add(Domain.Dish item)
        {
            var priceConfiguration = EntityFrom(item);
            using (var connection = database.CreateConnection())
            {
                connection.Open();

                connection.BulkInsert(priceConfiguration);
            }
        }

        public void Update(Domain.Dish item)
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(string id)
        {
            return Get(id) != null;
        }

        private List<Price> EntityFrom(Domain.Dish model)
        {
            return model.PriceConfiguration
                .Select(p => new Price(model.Id, p.ValidityStartDate, p.ValidityEndDate, p.Amount)).ToList();
        }

        private Domain.Dish ModelFrom(Dish entity)
        {
            var prices = entity.Prices.Select(p => new Domain.Price(p.Amount, p.ValidityStartDate, p.ValidityEndDate)).ToList();
            return new Domain.Dish(entity.Id, prices);
        }
    }
}
