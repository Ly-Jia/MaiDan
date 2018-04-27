using MaiDan.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Billing.Dal.Repositories
{
    public class TaxRateList : IRepository<Domain.TaxRate>
    {
        private IRepository<Domain.Tax> taxConfiguration;
        public TaxRateList(IRepository<Domain.Tax> taxConfiguration)
        {
            this.taxConfiguration = taxConfiguration;
        }

        public Domain.TaxRate Get(object id)
        {
            var idString = (string)id;
            using (var context = new BillingContext())
            {
                var entity = context.TaxRates
                    .AsNoTracking()
                    .FirstOrDefault(e => e.Id == idString);

                return entity == null ? null :
                    new Domain.TaxRate(entity.Id, taxConfiguration.Get(entity.TaxId), entity.Rate, entity.ValidityStartDate, entity.ValidityEndDate);
            }
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
