using System;
using MaiDan.Accounting.Domain;

namespace MaiDan.Infrastructure.Database
{
    public interface ICalendar
    {
        Day Get(DateTime date);

        Day GetCurrentDay();

        bool HasOpenedDay();

        void Add(Day day);

        void Update(Day day);

        bool Contains(DateTime date);
    }
}
