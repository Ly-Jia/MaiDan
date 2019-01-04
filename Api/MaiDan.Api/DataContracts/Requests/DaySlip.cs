using System;

namespace MaiDan.Api.DataContracts.Requests
{
    public class DaySlip
    {
        public DateTime Day { get; }
        public decimal CashAmount { get; }

        public Accounting.Domain.DaySlip ToDaySlip(Accounting.Domain.Day day)
        {
            return new Accounting.Domain.DaySlip(0, day, DateTime.Now, CashAmount);
        }
    }
}
