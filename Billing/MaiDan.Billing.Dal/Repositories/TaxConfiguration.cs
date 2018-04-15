using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using TaxRate = MaiDan.Billing.Dal.Entities.TaxRate;

namespace MaiDan.Billing.Dal.Repositories
{
    public class TaxConfiguration : IRepository<Domain.Tax>
    {
        private IDatabase database;

        public TaxConfiguration(IDatabase database)
        {
            this.database = database;
        }

        public Domain.Tax Get(object id)
        {
            Domain.Tax tax;

            var sql = "SELECT * " +
                      "FROM [TaxRate] tr " +
                      "WHERE tr.TaxId = @Id;";


            using (var connection = database.CreateConnection())
            {
                connection.Open();


                var taxRates = connection.Query<TaxRate>(sql,
                    param: new { Id = id });


                tax = new Domain.Tax(id as string, new List<Domain.TaxRate>());
                
                tax.TaxConfiguration.AddRange(taxRates.Select(t => new Domain.TaxRate(t.Id, tax, t.Rate, t.ValidityStartDate, t.ValidityEndDate)).ToList());
            }
            return tax;
        }

        public List<Domain.Tax> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Domain.Tax item)
        {
            throw new NotImplementedException();
        }

        public void Update(Domain.Tax item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(object id)
        {
            throw new NotImplementedException();
        }
    }
}
