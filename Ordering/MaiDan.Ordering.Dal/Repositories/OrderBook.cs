using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using Z.Dapper.Plus;
using Dish = MaiDan.Ordering.Dal.Entities.Dish;
using Line = MaiDan.Ordering.Dal.Entities.Line;
using Order = MaiDan.Ordering.Dal.Entities.Order;

namespace MaiDan.Ordering.Dal.Repositories
{
    public class OrderBook : IRepository<Domain.Order>
    {
        private IDatabase database;

        public OrderBook(IDatabase database)
        {
            this.database = database;
        }

        public Domain.Order Get(object id)
        {
            var sql = "SELECT * " +
                       "FROM \"Order\" o " +
                       "JOIN \"OrderLine\" l ON o.Id = l.OrderId " +
                       "JOIN \"Dish\" d ON l.DishId = d.Id " +
                       "WHERE o.Id = @Id;";

            using (var connection = database.CreateConnection())
            {
                connection.Open();

                var orderDictionary = new Dictionary<int, Order>();

                var order = connection.Query<Order, Line, Dish, Order>(
                        sql,
                        (o, l, d) =>
                        {
                            if (!orderDictionary.TryGetValue(o.Id, out var orderEntry))
                            {
                                orderEntry = o;
                                orderEntry.Lines = new List<Line>();
                                orderDictionary.Add(orderEntry.Id, orderEntry);
                            }

                            orderEntry.Lines.Add(new Line(o.Id, l.Index, l.Quantity, d));
                            return orderEntry;
                        },
                        param: new { Id = id },
                        splitOn: "Id,Id")
                    .FirstOrDefault();

                return order == null ? null : ModelFrom(order);
            }
        }

        public List<Domain.Order> GetAll()
        {
            string sql = "SELECT * " +
                         "FROM \"Order\" o " +
                         "LEFT OUTER JOIN \"Bill\" b ON o.Id = b.Id " +
                         "JOIN \"OrderLine\" l ON o.Id = l.OrderId " +
                         "JOIN \"Dish\" d ON l.DishId = d.Id " +
                         "WHERE b.Id IS NULL";

            List<Order> orders;

            using (var connection = database.CreateConnection())
            {
                connection.Open();

                var orderDictionary = new Dictionary<int, Order>();

                orders = connection.Query<Order, Line, Dish, Order>(
                        sql,
                        (o, l, d) =>
                        {
                            if (!orderDictionary.TryGetValue(o.Id, out var orderEntry))
                            {
                                orderEntry = o;
                                orderEntry.Lines = new List<Line>();
                                orderDictionary.Add(orderEntry.Id, orderEntry);
                            }

                            orderEntry.Lines.Add(new Line(o.Id, l.Index, l.Quantity, d));
                            return orderEntry;
                        },
                        splitOn: "Id,Id")
                    .Distinct()
                    .ToList();
            }

            return orders.Select(ModelFrom).ToList();
        }

        public void Add(Domain.Order item)
        {
            using (var connection = database.CreateConnection())
            {
                connection.Open();

                var entity = EntityFrom(item);
                connection.BulkInsert(entity)
                    .ThenForEach(x => x.Lines.ForEach(y => y.OrderId = x.Id))
                    .ThenBulkInsert(x => x.Lines);
            }
        }

        public void Update(Domain.Order item)
        {
            using (var connection = database.CreateConnection())
            {
                connection.Open();

                connection.BulkUpdate(EntityFrom(item), x => x.Lines);
            }
        }

        public bool Contains(object id)
        {
            return Get(id) != null;
        }

        private Order EntityFrom(Domain.Order model)
        {
            var lines = model.Lines.Select(l => new Line(model.Id, l.Id, l.Quantity, new Dish(l.Dish.Id, l.Dish.Name, l.Dish.Type))).ToList();
            if (model is OnSiteOrder)
            {
                var onSite = (OnSiteOrder)model;
                return new Order(model.Id, false, onSite.Table.Id, onSite.NumberOfGuests, lines);
            }

            return new Order(model.Id, true, null, 0, lines);
        }

        private Domain.Order ModelFrom(Order entity)
        {
            var lines = entity.Lines.Select(l => new Domain.Line(l.Index, l.Quantity, new Domain.Dish(l.Dish.Id, l.Dish.Name, l.Dish.Type))).ToList();

            if (entity.TakeAway)
                return new TakeAwayOrder(entity.Id, lines);

            return new OnSiteOrder(entity.Id, new Table(entity.TableId), entity.NumberOfGuests, lines);
        }
    }
}
