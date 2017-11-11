using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Dal.Entities;
using Z.Dapper.Plus;

namespace MaiDan.Ordering.Dal.Repositories
{
	public class OrderBook : IRepository<Domain.Order>
	{
	    private IDatabase database;

        public OrderBook(IDatabase database)
        {
            this.database = database;
        }

	    public Domain.Order Get(string id)
	    {
	        var sql = "SELECT * FROM \"OrderLine\" WHERE OrderId = @OrderId;";

	        var parsedId = Int32.Parse(id);

            using (var connection = database.CreateConnection())
	        {
	            connection.Open();
	            
	            var lines = connection.Query<Line>(sql, new {OrderId = parsedId}).ToList();
                
                return ModelFrom(new Order(parsedId, lines));
	        }
        }

	    public List<Domain.Order> GetAll()
	    {
	        string sql = "SELECT * FROM \"Order\" o INNER JOIN \"OrderLine\" l ON o.Id = l.OrderId;";
	        List<Order> orders;

            using (var connection = database.CreateConnection())
            {
	            connection.Open();

                connection.Open();

                var orderDictionary = new Dictionary<int, Order>();

                orders = connection.Query<Order, Line, Order>(
                        sql,
                        (o, l) =>
                        {
                            if (!orderDictionary.TryGetValue(o.Id, out var orderEntry))
                            {
                                orderEntry = o;
                                orderEntry.Lines = new List<Line>();
                                orderDictionary.Add(orderEntry.Id, orderEntry);
                            }

                            orderEntry.Lines.Add(l);
                            return orderEntry;
                        },
                        splitOn: "OrderId")
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

	    public bool Contains(string id)
	    {
	        return Get(id) != null;
	    }

        private Order EntityFrom(Domain.Order model)
	    {
	        var lines = model.Lines.Select(l => new Line(Int32.Parse(model.Id), l.Quantity, l.DishId)).ToList();
	        return new Order(Int32.Parse(model.Id), lines);
	    }

	    private Domain.Order ModelFrom(Order entity)
	    {
	        var lines = entity.Lines.Select(l => new Domain.Line(l.Quantity, l.DishId)).ToList();
	        return new Domain.Order(entity.Id.ToString(), lines);
	    }
	}
}
