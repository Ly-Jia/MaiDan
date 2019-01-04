using MaiDan.Accounting.Dal.Entities;
using MaiDan.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace MaiDan.Accounting.Dal.Repositories
{
    public class Calendar : ICalendar
    {
        private readonly AccountingContext context;
        private readonly ILogger<AccountingContext> logger;

        public Calendar(AccountingContext context, ILogger<AccountingContext> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public Domain.Day Get(DateTime date)
        {
            var entity = context.Days
                .AsNoTracking()
                .SingleOrDefault(e => e.Date == date);

            return entity == null ? null : ModelFrom(entity);
        }

        public Domain.Day GetCurrentDay()
        {
            var entity = context.Days
                .AsNoTracking()
                .SingleOrDefault(e => !e.Closed);

            return entity == null ? null : ModelFrom(entity);
        }

        public void Add(Domain.Day day)
        {
            var entity = EntityFrom(day);

            logger.Log(context, "Day", "Add", entity);

            context.Days.Add(entity);
            context.SaveChanges();
        }

        public void Update(Domain.Day day)
        {
            var entity = EntityFrom(day);
            var existingEntity = context.Days.FirstOrDefault(e => e.Date == entity.Date) ??
                throw new ArgumentException($"The day {entity.Date} was not found");

            logger.Log(context, "Day", "Update", existingEntity, entity);

            context.Entry(existingEntity).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }

        public bool Contains(DateTime date)
        {
            return context.Days.Any(e => e.Date == date);
        }

        private Day EntityFrom(Domain.Day model)
        {
            return new Day(model.Date, model.Closed);
        }

        private Domain.Day ModelFrom(Entities.Day entity)
        {
            return new Domain.Day(entity.Date, entity.Closed);
        }
    }
}
