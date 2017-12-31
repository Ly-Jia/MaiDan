using System;
using System.Collections.Generic;
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

                var entity = new Bill(item);
                connection.BulkInsert(entity)
                    .ThenBulkInsert(x => x.Lines);
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
