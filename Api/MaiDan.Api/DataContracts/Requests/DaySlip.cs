using System;

namespace MaiDan.Api.DataContracts.Requests
{
    public class DaySlip
    {
        public int Id { get; }
        public DateTime Day { get; }
        public decimal CashAmount { get; }

        public Accounting.Domain.DaySlip ToDaySlip(Accounting.Domain.Day day)
        {
            return new Accounting.Domain.DaySlip(Id, day, DateTime.Now, CashAmount);
        }
    }
}
