using System;
using System.Collections.Generic;
using Dapper;
using Dapper.Contrib.Extensions;
using MaiDan.Infrastructure.Database;
using MaiDan.Billing.Dal.Entities;
using Z.Dapper.Plus;

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
            throw new NotImplementedException();
        }

        public List<Domain.Bill> GetAll()
        {
            throw new NotImplementedException();
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
                        Index = i,
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
    }
}
