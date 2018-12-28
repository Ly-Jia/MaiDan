using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Ordering.Dal.Repositories
{
    public class Room : IRepository<Domain.Table>
    {
        private readonly OrderingContext context;
        private readonly ILogger<OrderingContext> logger;

        public Room(OrderingContext context, ILogger<OrderingContext> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Domain.Table Get(object id)
        {
            var idString = (string)id;
            var entity = context.Tables
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == idString);

            return entity == null ? null : ModelFrom(entity);
        }

        public IEnumerable<Domain.Table> GetAll()
        {
            var entities = context.Tables
                .AsNoTracking();

            return entities.Select(ModelFrom).ToArray();
        }

        public object Add(Domain.Table item)
        {
            var entity = EntityFrom(item);

            logger.Log(context, "Table", "Add", entity);

            context.Tables.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }

        public void Update(Domain.Table item)
        {
            var entity = EntityFrom(item);
            var existingEntity = context.Tables.FirstOrDefault(e => e.Id == entity.Id) ??
                throw new ArgumentException($"The table {entity.Id} was not found");

            logger.Log(context, "Table", "Update", existingEntity, entity);

            context.Entry(existingEntity).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }

        public bool Contains(object id)
        {
            var idString = (string)id;
            return context.Tables.Any(e => e.Id == idString);
        }

        private Table EntityFrom(Domain.Table model)
        {
            return new Table(model.Id);
        }

        private Domain.Table ModelFrom(Table entity)
        {
            return new Domain.Table(entity.Id);
        }
    }
}
