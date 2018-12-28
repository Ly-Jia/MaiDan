using System;
using System.Collections.Generic;
using System.Linq;
using MaiDan.Accounting.Dal.Entities;
using MaiDan.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MaiDan.Accounting.Dal.Repositories
{
    public class DaySlipBook : IRepository<Domain.DaySlip>
    {
        private readonly AccountingContext context;
        private readonly ILogger<AccountingContext> logger;

        public DaySlipBook(AccountingContext context, ILogger<AccountingContext> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Domain.DaySlip Get(object id)
        {
            var idInt = (int)id;
            var entity = context.DaySlips
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == idInt);

            return entity == null ? null : ModelFrom(entity);
        }

        public IEnumerable<Domain.DaySlip> GetAll()
        {
            var entities = context.DaySlips
                .AsNoTracking()
                .OrderByDescending(s => s.Id);

            return entities.Select(ModelFrom).ToArray();
        }

        public object Add(Domain.DaySlip item)
        {
            var entity = EntityFrom(item);

            logger.Log(context, "DaySlip", "Add", entity);

            context.DaySlips.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }

        public void Update(Domain.DaySlip item)
        {
            var entity = EntityFrom(item);
            var existingEntity = context.Slips
                                     .Include(e => e.Payments)
                                     .FirstOrDefault(e => e.Id == entity.Id) ??
                                 throw new ArgumentException($"The dayslip {entity.Id} was not found");

            logger.Log(context, "DaySlip", "Update", existingEntity, entity);

            context.Entry(existingEntity).CurrentValues.SetValues(entity);
            existingEntity.Payments.Clear();
            context.SaveChanges();
        }

        public bool Contains(object id)
        {
            throw new NotImplementedException();
        }

        public DaySlip EntityFrom(Domain.DaySlip model)
        {
            return new DaySlip(model.Id, model.Day, model.ClosingDate, model.CashAmount);
        }

        public Domain.DaySlip ModelFrom(DaySlip entity)
        {
            return new Domain.DaySlip(entity.Id, entity.Day, entity.ClosingDate, entity.CashAmount);
        }
    }
}
