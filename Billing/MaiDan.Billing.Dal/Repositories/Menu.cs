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

        public Domain.Dish Get(object id)
        {
            string sql = "SELECT dish.Id, dish.Type, price.DishId, price.ValidityStartDate, price.ValidityEndDate, price.Amount " +
                         "FROM \"Dish\" dish " +
                         "JOIN \"DishPrice\" price ON dish.Id = price.DishId " +
                         $"WHERE dish.Id = @Id;";

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
                        param: new { Id = id },
                        splitOn: "DishId")
                    .FirstOrDefault();

                return dish == null ? null : ModelFrom(dish);
            }
        }

        public List<Domain.Dish> GetAll()
        {
            const string sql = "SELECT dish.Id, dish.Type, price.DishId, price.ValidityStartDate, price.ValidityEndDate, price.Amount " +
                               "FROM \"Dish\" dish " +
                               "JOIN \"DishPrice\" price ON dish.Id = price.DishId ";

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

        public bool Contains(object id)
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
            return new Domain.Dish(entity.Id, prices, null);
        }
    }
}
