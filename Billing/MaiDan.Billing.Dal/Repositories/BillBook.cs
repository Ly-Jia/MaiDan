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

        public BillBook(BillingContext context, IRepository<Domain.TaxRate> taxRateList)
        {
            this.context = context;
            this.taxRateList = taxRateList;
        }

        public Domain.Bill Get(object id)
        {
            var idInt = (int)id;
            var entity = context.Bills
                .AsNoTracking()
                .Include(e => e.Lines)
                .Include(e => e.Taxes)
                .FirstOrDefault(e => e.Id == idInt);

            return entity == null ? null : ModelFrom(entity);
        }

        public List<Domain.Bill> GetAll()
        {
            var entities = context.Bills
                .AsNoTracking()
                .Include(e => e.Lines)
                .Include(e => e.Taxes);

            return entities.Select(ModelFrom).ToList();
        }

        public void Add(Domain.Bill item)
        {
            var entity = EntityFrom(item);
            context.Bills.Add(entity);
            context.SaveChanges();
        }

        public void Update(Domain.Bill item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(object id)
        {
            var idInt = (int)id;
            return context.Bills
                .AsNoTracking()
                .Any(e => e.Id == idInt);
        }

        private Bill EntityFrom(Domain.Bill model)
        {
            var lines = model.Lines.Select(l => new Line(model.Id, l.Id, l.Amount, new TaxRate(l.TaxRate), l.TaxAmount)).ToList();
            var taxes = model.Taxes.Select(t => new BillTax(model.Id, t.Id, new TaxRate(t.TaxRate), t.Amount)).ToList();

            return new Bill(model.Id, model.Total, lines, taxes);
        }

        private Domain.Bill ModelFrom(Bill entity)
        {
            var lines = entity.Lines.Select(l => new Domain.Line(l.Index, l.Amount, taxRateList.Get(l.TaxRate.Id), l.TaxRate.Rate)).ToList();
            var taxes = entity.Taxes.Select(t => new Domain.BillTax(t.Index, taxRateList.Get(t.TaxRate.Id), t.Amount)).ToList();

            return new Domain.Bill(entity.Id, lines, entity.Total, taxes);
        }
    }
}
