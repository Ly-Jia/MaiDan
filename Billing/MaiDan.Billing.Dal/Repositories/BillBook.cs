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
        private readonly ILogger<BillingContext> logger;

        public BillBook(BillingContext context, IRepository<Domain.TaxRate> taxRateList, IRepository<Domain.Discount> discountList, ILogger<BillingContext> logger)
        {
            this.context = context;
            this.taxRateList = taxRateList;
            this.discountList = discountList;
            this.logger = logger;
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
                .AsNoTracking()
                .Where(e => !e.Closed);

            return entities.Select(ModelFrom).ToArray();
        }

        public object Add(Domain.Bill item)
        {
            var entity = EntityFrom(item);

            logger.Log(context, "Bill", "Add", entity);

            context.Bills.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }

        public void Update(Domain.Bill item)
        {
            var entity = EntityFrom(item);
            var existingEntity = context.Bills
                                     .Include(e => e.Lines)
                                     .FirstOrDefault(e => e.Id == entity.Id) ??
                                 throw new ArgumentException($"The bill {entity.Id} was not found");

            logger.Log(context, "Bill", "Update", existingEntity, entity);

            context.Entry(existingEntity).CurrentValues.SetValues(entity);
            existingEntity.Lines.Clear();
            existingEntity.Lines.AddRange(entity.Lines);
            context.SaveChanges();
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

            return new Bill(model.Id, model.BillingDate, model.Total, lines, discounts, taxes, model.Closed);
        }

        private Domain.Bill ModelFrom(Bill entity)
        {
            var lines = entity.Lines.Select(l => new Domain.Line(l.Index, l.Amount, taxRateList.Get(l.TaxRate.Id))).ToList();
            var discounts = entity.Discounts.ToDictionary(d => discountList.Get(d.DiscountId), d => d.Amount);
            var taxes = entity.Taxes.ToDictionary(t => taxRateList.Get(t.TaxRateId), t => t.Amount);

            return new Domain.Bill(entity.Id, entity.BillingDate, lines, discounts, entity.Total, taxes, entity.Closed);
        }
    }
}
