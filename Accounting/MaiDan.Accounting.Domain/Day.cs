using System;

namespace MaiDan.Accounting.Domain
{
    public class Day
    {
        public Day(DateTime date, bool closed)
        {
            Date = date;
            Closed = closed;
        }

        public DateTime Date { get; private set; }
        public bool Closed { get; private set; }

        public void Close()
        {
            Closed = true;
        }

        public void Open()
        {
            Closed = false;
        }
    }
}
