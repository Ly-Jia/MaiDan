using System;

namespace MaiDan.Api.DataContracts.Requests
{
    public class DaySlip
    {
        public DateTime Day { get; set; }
        public decimal CashAmount { get; set; }

        public Accounting.Domain.DaySlip ToDaySlip(Accounting.Domain.Day day)
        {
            return new Accounting.Domain.DaySlip(0, day, DateTime.Now, CashAmount);
        }
    }
}
