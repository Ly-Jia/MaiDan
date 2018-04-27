﻿using MaiDan.Billing.Dal.Entities;
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

        public Menu(BillingContext context)
        {
            this.context = context;
        }

        public Domain.Dish Get(object id)
        {
            var idString = (string)id;
            var entity = context.Dishes
                .AsNoTracking()
                .Include(e => e.Prices)
                .FirstOrDefault(e => e.Id == idString);

            return entity == null ? null : ModelFrom(entity);
        }

        public List<Domain.Dish> GetAll()
        {
            var entities = context.Dishes
                .AsNoTracking()
                .Include(e => e.Prices);

            return entities.Select(ModelFrom).ToList();
        }

        public object Add(Domain.Dish item)
        {
            var entity = EntityFrom(item);
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

            context.Entry(existingEntity).CurrentValues.SetValues(entity);
            existingEntity.Prices.Clear();
            existingEntity.Prices.AddRange(entity.Prices);

            context.SaveChanges();
        }

        public bool Contains(object id)
        {
            var idString = (string)id;
            return context.Dishes
                .AsNoTracking()
                .Any(e => e.Id == idString);
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
