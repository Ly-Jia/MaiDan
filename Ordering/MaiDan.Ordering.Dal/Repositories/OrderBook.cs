using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Dish = MaiDan.Ordering.Dal.Entities.Dish;
using Line = MaiDan.Ordering.Dal.Entities.Line;
using Order = MaiDan.Ordering.Dal.Entities.Order;

namespace MaiDan.Ordering.Dal.Repositories
{
    public class OrderBook : IRepository<Domain.Order>
    {
        private readonly OrderingContext context;

        public OrderBook(OrderingContext context)
        {
            this.context = context;
        }

        public Domain.Order Get(object id)
        {
            var idInt = (int)id;
            var entity = context.Orders
                .AsNoTracking()
                .Include(e => e.Table)
                .Include(e => e.Lines)
                .ThenInclude(e => e.Dish)
                .FirstOrDefault(e => e.Id == idInt);

            return entity == null ? null : ModelFrom(entity);
        }

        public List<Domain.Order> GetAll()
        {
            var entities = context.Orders
                .AsNoTracking()
                .Include(e => e.Table)
                .Include(e => e.Lines)
                .ThenInclude(e => e.Dish);

            return entities.Select(ModelFrom).ToList();
        }

        public object Add(Domain.Order item)
        {
            var entity = EntityFrom(item);
            context.Orders.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }

        public void Update(Domain.Order item)
        {
            var entity = EntityFrom(item);
            var existingEntity = context.Orders
                .Include(e => e.Lines)
                .FirstOrDefault(e => e.Id == entity.Id) ??
                throw new ArgumentException($"The order {entity.Id} was not found");

            context.Entry(existingEntity).CurrentValues.SetValues(entity);
            existingEntity.Table = entity.Table;
            existingEntity.Lines.Clear();
            existingEntity.Lines.AddRange(entity.Lines);

            context.SaveChanges();
        }

        public bool Contains(object id)
        {
            var idInt = (int)id;
            return context.Orders
                .AsNoTracking()
                .Any(e => e.Id == idInt);
        }

        private Order EntityFrom(Domain.Order model)
        {
            var lines = model.Lines.Select(l => new Line(model.Id, l.Id, l.Quantity, new Dish(l.Dish.Id, l.Dish.Name))).ToList();

            if (!(model is OnSiteOrder onSite))
            {
                return new Order(model.Id, true, null, 0, lines);
            }

            var table = context.Tables
                .FirstOrDefault(e => e.Id == onSite.Table.Id) ??
                throw new ArgumentException($"The table {onSite.Table.Id} was not found");

            return new Order(model.Id, false, table, onSite.NumberOfGuests, lines);
        }

        private Domain.Order ModelFrom(Order entity)
        {
            var lines = entity.Lines.Select(l => new Domain.Line(l.Index, l.Quantity, new Domain.Dish(l.Dish.Id, l.Dish.Name))).ToList();

            if (entity.TakeAway)
            {
                return new TakeAwayOrder(entity.Id, lines);
            }

            return new OnSiteOrder(entity.Id, new Domain.Table(entity.Table.Id), entity.NumberOfGuests, lines);
        }
    }
}
