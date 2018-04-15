using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using MaiDan.Billing.Dal.Entities;
using MaiDan.Infrastructure.Database;

namespace MaiDan.Billing.Dal.Repositories
{
    public class BillBook : IRepository<Domain.Bill>
    {
        private readonly IDatabase database;
        private IRepository<Domain.TaxRate> taxRateList;

        public BillBook(IDatabase database, IRepository<Domain.TaxRate> taxRateList)
        {
            this.database = database;
            this.taxRateList = taxRateList;
        }

        public Domain.Bill Get(object id)
        {
            var sql = "SELECT * " +
                      "FROM [Bill] b " +
                      "JOIN [BillLine] l ON b.Id = l.BillId " +
                      "JOIN [BillTax] t ON b.Id = t.BillId " +
                      "WHERE b.Id = @Id;";

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

                            billEntry.Lines.Add(new Line(b.Id, l.Index, l.Amount, l.TaxRate, l.TaxAmount));
                            return billEntry;
                        },
                        param: new { Id = id },
                        splitOn: "Id,Id")
                    .FirstOrDefault();

                return bill == null ? null : ModelFrom(bill);
            }
        }

        public List<Domain.Bill> GetAll()
        {
            string sql = "SELECT * " +
                         "FROM [Bill] b " +
                         "JOIN [BillLine] l ON b.Id = l.BillId " +
                         "JOIN [BillTax] t ON b.Id = t.BillId ";

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
                                billEntry = new Bill { Id = b.Id, Lines = new List<Line>() };
                                orderDictionary.Add(billEntry.Id, billEntry);
                            }

                            billEntry.Lines.Add(new Line(b.Id, l.Index, l.Amount, l.TaxRate, l.TaxAmount));
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

                var entity = EntityFrom(item);
                connection.Insert(entity);
            }
        }

        public void Update(Domain.Bill item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(object id)
        {
            return Get(id) != null;
        }

        private Bill EntityFrom(Domain.Bill model)
        {
            var lines = model.Lines.Select(l => new Line(model.Id, l.Id, l.Amount, new TaxRate(l.TaxRate), l.TaxAmount)).ToList();
            var taxes = model.Taxes.Select(t => new BillTax(model.Id, t.Id, new TaxRate(t.TaxRate),  t.Amount)).ToList();

            return new Bill(model.Id, model.Total, lines, taxes);
        }

        private Domain.Bill ModelFrom(Bill entity)
        {
            var lines = entity.Lines.Select(l => new Domain.Line(l.Index, l.Amount, taxRateList.Get(l.TaxRate.Id), l.TaxRate.Rate)).ToList();
            var taxes = entity.Taxes.Select(t => new Domain.BillTax(t.Index, taxRateList.Get(t.TaxRate.Id), t.Amount)).ToList();

            return new Domain.Bill(entity.Id, lines, entity.Total, taxes);
        }
    }
}
