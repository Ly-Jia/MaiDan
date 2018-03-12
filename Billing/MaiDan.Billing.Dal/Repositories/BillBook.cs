using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using MaiDan.Infrastructure.Database;
using MaiDan.Billing.Dal.Entities;

namespace MaiDan.Billing.Dal.Repositories
{
    public class BillBook : IRepository<Domain.Bill>
    {
        private IDatabase database;

        public BillBook(IDatabase database)
        {
            this.database = database;
        }

        public Domain.Bill Get(string id)
        {
            var parsedId = Int32.Parse(id);
            var sql = "SELECT * " +
                      "FROM \"Bill\" b " +
                      "JOIN \"BillLine\" l ON b.Id = l.BillId " +
                      $"WHERE b.Id = {parsedId};";

            using (var connection = database.CreateConnection())
            {
                connection.Open();

                var billDictionary = new Dictionary<int, Bill>();

                var bill = connection.Query<Bill, Line, Bill>(
                        sql,
                        (b, l) =>
                        {
                            if (!billDictionary.TryGetValue(b.Id, out var billEntry))
                            {
                                billEntry = b;
                                billEntry.Lines = new List<Line>();
                                billDictionary.Add(billEntry.Id, billEntry);
                            }

                            billEntry.Lines.Add(new Line(b.Id, l.Index, l.Amount));
                            return billEntry;
                        },
                        splitOn: "Id,Id")
                    .Distinct()
                    .Single();

                return ModelFrom(bill);
            }
        }

        public List<Domain.Bill> GetAll()
        {
            string sql = "SELECT *  " +
                         "FROM \"Bill\" b " +
                         "JOIN \"BillLine\" l ON b.Id = l.BillId ";

            List<Bill> bills;

            using (var connection = database.CreateConnection())
            {
                connection.Open();

                var orderDictionary = new Dictionary<int, Bill>();

                bills = connection.Query<Bill, Line, Bill>(
                        sql,
                        (b, l) =>
                        {
                            if (!orderDictionary.TryGetValue(b.Id, out var billEntry))
                            {
                                billEntry = new Bill {Id = b.Id, Lines = new List<Line>()};
                                orderDictionary.Add(billEntry.Id, billEntry);
                            }

                            billEntry.Lines.Add(new Line(b.Id, l.Index, l.Amount));
                            return billEntry;
                        },
                        splitOn: "Id,Id")
                    .Distinct()
                    .ToList();
            }

            return bills.Select(ModelFrom).ToList();
        }

        public void Add(Domain.Bill item)
        {
            using (var connection = database.CreateConnection())
            {
                connection.Open();
                
                connection.Insert(new Bill { Id = item.Id, Total = item.Total });
                for (int i = 0; i < item.Lines.Count; i++)
                {
                    connection.Insert(new Line
                    {
                        Id = $"{item.Id}-{i}",
                        BillId = item.Id,
                        Index = i+1,
                        Amount = item.Lines[i].Amount
                    });
                }

            }
        }

        public void Update(Domain.Bill item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(string id)
        {
            throw new NotImplementedException();
        }

        private Domain.Bill ModelFrom(Bill entity)
        {
            var lines = entity.Lines.Select(l => new Domain.Line(l.Index, l.Amount, null, 0m)).ToList();

            return new Domain.Bill(entity.Id, lines);
        }
    }
}
