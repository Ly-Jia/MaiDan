using MaiDan.Billing.Dal.Entities;
using MaiDan.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Billing.Dal.Repositories
{
    public class BillBook : IRepository<Domain.Bill>
    {
        private readonly BillingContext context;
        private readonly IRepository<Domain.TaxRate> taxRateList;
        private readonly IRepository<Domain.Discount> discountList;

        public BillBook(BillingContext context, IRepository<Domain.TaxRate> taxRateList, IRepository<Domain.Discount> discountList)
        {
            this.context = context;
            this.taxRateList = taxRateList;
            this.discountList = discountList;
        }

        public Domain.Bill Get(object id)
        {
            var idInt = (int)id;
            var entity = context.Bills
                .Include(e => e.Lines)
                .ThenInclude(e => e.TaxRate)
                .Include(e => e.Taxes)
                .Include(e => e.Discounts)
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == idInt);

            return entity == null ? null : ModelFrom(entity);
        }

        public IEnumerable<Domain.Bill> GetAll()
        {
            var entities = context.Bills
                .Include(e => e.Lines)
                .ThenInclude(e => e.TaxRate)
                .Include(e => e.Taxes)
                .Include(e => e.Discounts)
                .AsNoTracking();

            return entities.Select(ModelFrom).ToArray();
        }

        public object Add(Domain.Bill item)
        {
            var entity = EntityFrom(item);
            context.Bills.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }

        public void Update(Domain.Bill item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(object id)
        {
            var idInt = (int)id;
            return context.Bills.Any(e => e.Id == idInt);
        }

        private Bill EntityFrom(Domain.Bill model)
        {
            var lines = model.Lines.Select(l => new Line(model.Id, l.Id, l.Amount, context.TaxRates.Find(l.TaxRate.Id))).ToList();
            var discounts = model.Discounts.Select(d => new BillDiscount(model.Id, d.Key.Id, d.Value)).ToList();
            var taxes = model.Taxes.Select(t => new BillTax(model.Id, t.Key.Id, t.Value)).ToList();

            return new Bill(model.Id, model.BillingDate, model.Total, lines, discounts, taxes);
        }

        private Domain.Bill ModelFrom(Bill entity)
        {
            var lines = entity.Lines.Select(l => new Domain.Line(l.Index, l.Amount, taxRateList.Get(l.TaxRate.Id))).ToList();
            var discounts = entity.Discounts.ToDictionary(d => discountList.Get(d.DiscountId), d => d.Amount);
            var taxes = entity.Taxes.ToDictionary(t => taxRateList.Get(t.TaxRateId), t => t.Amount);

            return new Domain.Bill(entity.Id, entity.BillingDate, lines, discounts, entity.Total, taxes);
        }
    }
}
