using System;

namespace MaiDan.Accounting.Domain
{
    public class DaySlip
    {
        public DaySlip(int id, Day day, DateTime closingDate, decimal cashAmount)
        {
            Id = id;
            Day = day;
            ClosingDate = closingDate;
            CashAmount = cashAmount;
        }

        public int Id { get; }
        public Day Day { get; }
        public DateTime ClosingDate { get; }
        public decimal CashAmount { get; }
    }
}
