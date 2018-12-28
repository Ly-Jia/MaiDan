using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Ordering.Dal.Repositories
{
    public class Menu : IRepository<Domain.Dish>
    {
        private readonly OrderingContext context;
        private readonly ILogger<OrderingContext> logger;

        public Menu(OrderingContext context, ILogger<OrderingContext> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Domain.Dish Get(object id)
        {
            var idString = (string)id;
            var entity = context.Dishes
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == idString);

            return entity == null ? null : ModelFrom(entity);
        }

        public IEnumerable<Domain.Dish> GetAll()
        {
            var entities = context.Dishes
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
            var existingEntity = context.Dishes.FirstOrDefault(e => e.Id == entity.Id) ??
                throw new ArgumentException($"The dish {entity.Id} was not found");

            logger.Log(context, "Dish", "Update", existingEntity, entity);

            context.Entry(existingEntity).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }

        public bool Contains(object id)
        {
            var idString = (string)id;
            return context.Dishes.Any(e => e.Id == idString);
        }

        private Dish EntityFrom(Domain.Dish model)
        {
            return new Dish(model.Id, model.Name);
        }

        private Domain.Dish ModelFrom(Dish entity)
        {
            return new Domain.Dish(entity.Id, entity.Name);
        }
    }
}
