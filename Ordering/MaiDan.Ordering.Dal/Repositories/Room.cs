﻿using MaiDan.Infrastructure.Database;
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

        public Room(OrderingContext context)
        {
            this.context = context;
        }

        public Domain.Table Get(object id)
        {
            var idString = (string)id;
            var entity = context.Tables
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == idString);

            return entity == null ? null : ModelFrom(entity);
        }

        public List<Domain.Table> GetAll()
        {
            var entities = context.Tables
                .AsNoTracking();

            return entities.Select(ModelFrom).ToList();
        }

        public void Add(Domain.Table item)
        {
            var entity = EntityFrom(item);
            context.Tables.Add(entity);
            context.SaveChanges();
        }

        public void Update(Domain.Table item)
        {
            var entity = EntityFrom(item);
            var existingEntity = context.Tables.FirstOrDefault(e => e.Id == entity.Id) ??
                throw new ArgumentException($"The table {entity.Id} was not found");

            context.Entry(existingEntity).CurrentValues.SetValues(entity);

            context.SaveChanges();
        }

        public bool Contains(object id)
        {
            var idString = (string)id;
            return context.Tables
                .AsNoTracking()
                .Any(e => e.Id == idString);
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
