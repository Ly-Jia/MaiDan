using System;
using System.Collections.Generic;
using MaiDan.Infrastructure.Database;

namespace MaiDan.Billing.Dal.Repositories
{
    public class TaxConfiguration : IRepository<Domain.Tax>
    {
        private readonly IDatabase database;

        public TaxConfiguration(IDatabase database)
        {
            this.database = database;
        }

        public Domain.Tax Get(object id)
        {
            throw new NotImplementedException();
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
