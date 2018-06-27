using System;
using System.Collections.Generic;
using System.Linq;
using MaiDan.Accounting.Dal.Entities;
using MaiDan.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MaiDan.Accounting.Dal.Repositories
{
    public class PaymentMethodList : IRepository<Domain.PaymentMethod>
    {
        private readonly AccountingContext context;

        public PaymentMethodList(AccountingContext context)
        {
            this.context = context;
        }

        public Domain.PaymentMethod Get(object id)
        {
            var idString = (string)id;
            var entity = context.PaymentMethods
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == idString);

            return entity == null ? null : ModelFrom(entity);
        }

        public IEnumerable<Domain.PaymentMethod> GetAll()
        {
            var entities = context.PaymentMethods
                .AsNoTracking();

            return entities.Select(ModelFrom).ToArray();
        }

        public object Add(Domain.PaymentMethod item)
        {
            throw new NotImplementedException();
        }

        public void Update(Domain.PaymentMethod item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(object id)
        {
            throw new NotImplementedException();
        }

        private PaymentMethod EntityFrom(Domain.PaymentMethod model)
        {
            return new PaymentMethod(model.Id, model.Name);
        }

        private Domain.PaymentMethod ModelFrom(PaymentMethod entity)
        {
            return new Domain.PaymentMethod(entity.Id, entity.Name);
        }
    }
}
