using MaiDan.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Billing.Dal.Repositories
{
    public class TaxConfiguration : IRepository<Domain.Tax>
    {
        public Domain.Tax Get(object id)
        {
            var idString = (string)id;
            using (var context = new BillingContext())
            {
                var entities = context.TaxRates
                    .AsNoTracking()
                    .Where(e => e.TaxId == idString);

                var tax = new Domain.Tax(idString, new List<Domain.TaxRate>());
                tax.TaxConfiguration.AddRange(entities.Select(t => new Domain.TaxRate(t.Id, tax, t.Rate, t.ValidityStartDate, t.ValidityEndDate)).ToList());
                return tax;
            }
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
