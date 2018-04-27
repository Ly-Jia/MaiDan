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
        public Domain.Dish Get(object id)
        {
            var idString = (string)id;
            using (var context = new OrderingContext())
            {
                var entity = context.Dishes
                    .AsNoTracking()
                    .FirstOrDefault(e => e.Id == idString);

                return entity == null ? null : ModelFrom(entity);
            }
        }

        public List<Domain.Dish> GetAll()
        {
            using (var context = new OrderingContext())
            {
                var entities = context.Dishes
                    .AsNoTracking();

                return entities.Select(ModelFrom).ToList();
            }
        }

        public void Add(Domain.Dish item)
        {
            var entity = EntityFrom(item);
            using (var context = new OrderingContext())
            {
                context.Dishes.Add(entity);
                context.SaveChanges();
            }
        }

        public void Update(Domain.Dish item)
        {
            var entity = EntityFrom(item);
            using (var context = new OrderingContext())
            {
                var existingEntity = context.Dishes.FirstOrDefault(e => e.Id == entity.Id) ??
                    throw new ArgumentException($"The dish {entity.Id} was not found");

                context.Entry(existingEntity).CurrentValues.SetValues(entity);

                context.SaveChanges();
            }
        }

        public bool Contains(object id)
        {
            var idString = (string)id;
            using (var context = new OrderingContext())
            {
                return context.Dishes
                    .AsNoTracking()
                    .Any(e => e.Id == idString);
            }
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
