using System;
using System.Collections.Generic;
using System.Linq;
using MaiDan.Accounting.Dal.Entities;
using MaiDan.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MaiDan.Accounting.Dal.Repositories
{
    public class SlipBook : IRepository<Domain.Slip>
    {
        private readonly AccountingContext context;
        private readonly IRepository<Domain.PaymentMethod> paymentMethodList;
        private readonly ILogger<AccountingContext> logger;

        public SlipBook(AccountingContext context, IRepository<Domain.PaymentMethod> paymentMethodList, ILogger<AccountingContext> logger)
        {
            this.context = context;
            this.paymentMethodList = paymentMethodList;
            this.logger = logger;
        }

        public Domain.Slip Get(object id)
        {
            var idInt = (int)id;
            var entity = context.Slips
                .Include(s => s.Payments)
                .ThenInclude(p => p.PaymentMethod)
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == idInt);

            return entity == null ? null : ModelFrom(entity);
        }

        public IEnumerable<Domain.Slip> GetAll()
        {
            var entities = context.Slips
                .Include(s => s.Payments)
                .ThenInclude(p => p.PaymentMethod)
                .AsNoTracking()
                .OrderByDescending(s => s.Id);

            return entities.Select(ModelFrom).ToArray();
        }

        public object Add(Domain.Slip item)
        {
            var entity = EntityFrom(item);

            logger.Log(context, "Slip", "Add", entity);

            context.Slips.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }

        public void Update(Domain.Slip item)
        {
            var entity = EntityFrom(item);
            var existingEntity = context.Slips
                                     .Include(e => e.Payments)
                                     .FirstOrDefault(e => e.Id == entity.Id) ??
                                 throw new ArgumentException($"The slip {entity.Id} was not found");

            logger.Log(context, "Slip", "Update", existingEntity, entity);

            context.Entry(existingEntity).CurrentValues.SetValues(entity);
            existingEntity.Payments.Clear();
            existingEntity.Payments.AddRange(entity.Payments);
            context.SaveChanges();
        }

        public bool Contains(object id)
        {
            throw new NotImplementedException();
        }

        public Slip EntityFrom(Domain.Slip model)
        {
            var payments = model.Payments.Select(p => new Payment(model.Id, p.Id, context.PaymentMethods.Find(p.Method.Id), p.Amount)).ToList();

            return new Slip(model.Id, model.PaymentDate, payments);
        }

        public Domain.Slip ModelFrom(Slip entity)
        {
            var payments = entity.Payments.Select(p => new Domain.Payment(p.Index, paymentMethodList.Get(p.PaymentMethod.Id), p.Amount)).ToList();

            return new Domain.Slip(entity.Id, entity.PaymentDate, payments);
        }
    }
}
