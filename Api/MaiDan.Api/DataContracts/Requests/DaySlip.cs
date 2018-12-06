using System;

namespace MaiDan.Api.DataContracts.Requests
{
    public class DaySlip
    {
        public int Id { get; }
        public DateTime Day { get; }
        public decimal CashAmount { get; }

        public Accounting.Domain.DaySlip ToDaySlip()
        {
            return new Accounting.Domain.DaySlip(Id, Day, DateTime.Now, CashAmount);
        }
    }
}
