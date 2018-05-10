using System;
using System.Collections.Generic;
using System.Linq;
using MaiDan.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MaiDan.Billing.Dal.Repositories
{
    public class DiscountList : IRepository<Domain.Discount>
    {
        private readonly BillingContext context;
        private readonly IRepository<Domain.Tax> taxConfiguration;

        public DiscountList(BillingContext context, IRepository<Domain.Tax> taxConfiguration)
        {
            this.context = context;
            this.taxConfiguration = taxConfiguration;
        }

        public Domain.Discount Get(object id)
        {
            var idString = (string)id;

            var entity = context.Discounts
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == idString);

            return entity == null ? null :
                new Domain.Discount(entity.Id, entity.Rate, taxConfiguration.Get(entity.ApplicableTaxId));
        }

        public IEnumerable<Domain.Discount> GetAll()
        {
            throw new NotImplementedException();
        }

        public object Add(Domain.Discount item)
        {
            throw new NotImplementedException();
        }

        public void Update(Domain.Discount item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(object id)
        {
            throw new NotImplementedException();
        }
    }
}
