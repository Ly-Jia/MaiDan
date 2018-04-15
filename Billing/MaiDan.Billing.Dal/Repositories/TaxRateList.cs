using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;
using MaiDan.Billing.Dal.Entities;
using MaiDan.Infrastructure.Database;

namespace MaiDan.Billing.Dal.Repositories
{
    public class TaxRateList : IRepository<Domain.TaxRate>
    {
        private IDatabase database;
        private IRepository<Domain.Tax> taxConfiguration;
        public TaxRateList(IDatabase database, IRepository<Domain.Tax> taxConfiguration)
        {
            this.database = database;
            this.taxConfiguration = taxConfiguration;
        }

        public Domain.TaxRate Get(object id)
        {
            TaxRate taxRate;
            using (var connection = database.CreateConnection())
            {
                taxRate = connection.Get<TaxRate>(id);
            }
            return new Domain.TaxRate(taxRate.Id, taxConfiguration.Get(taxRate.TaxId), taxRate.Rate, taxRate.ValidityStartDate, taxRate.ValidityEndDate);
        }

        public List<Domain.TaxRate> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Domain.TaxRate item)
        {
            throw new NotImplementedException();
        }

        public void Update(Domain.TaxRate item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(object id)
        {
            throw new NotImplementedException();
        }
    }
}
