using MaiDan.Billing.Dal.Entities;
using MaiDan.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Billing.Dal.Repositories
{
    public class Menu : IRepository<Domain.Dish>
    {
        private readonly BillingContext context;
        private readonly ILogger<BillingContext> logger;

        public Menu(BillingContext context, ILogger<BillingContext> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Domain.Dish Get(object id)
        {
            var idString = (string)id;
            var entity = context.Dishes
                .Include(e => e.Prices)
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == idString);

            return entity == null ? null : ModelFrom(entity);
        }

        public IEnumerable<Domain.Dish> GetAll()
        {
            var entities = context.Dishes
                .Include(e => e.Prices)
                .AsNoTracking();

            return entities.Select(ModelFrom).ToArray();
        }

        public object Add(Domain.Dish item)
        {
            var entity = EntityFrom(item);

            logger.Log(context, "Dish", "Add", entity);

            context.Dishes.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }

        public void Update(Domain.Dish item)
        {
            var entity = EntityFrom(item);
            var existingEntity = context.Dishes
                .Include(e => e.Prices)
                .FirstOrDefault(e => e.Id == entity.Id) ??
                throw new ArgumentException($"The dish {entity.Id} was not found");

            logger.Log(context, "Dish", "Update", existingEntity, entity);

            context.Entry(existingEntity).CurrentValues.SetValues(entity);
            existingEntity.Prices.Clear();
            existingEntity.Prices.AddRange(entity.Prices);
            context.SaveChanges();
        }

        public bool Contains(object id)
        {
            var idString = (string)id;
            return context.Dishes.Any(e => e.Id == idString);
        }

        private Dish EntityFrom(Domain.Dish model)
        {
            var prices = model.PriceConfiguration.Select(p => new Price(model.Id, p.ValidityStartDate, p.ValidityEndDate, p.Amount)).ToList();
            return new Dish(model.Id, prices, model.Type);
        }

        private Domain.Dish ModelFrom(Dish entity)
        {
            var prices = entity.Prices.Select(p => new Domain.Price(p.Amount, p.ValidityStartDate, p.ValidityEndDate)).ToList();
            return new Domain.Dish(entity.Id, prices, entity.Type);
        }
    }
}
