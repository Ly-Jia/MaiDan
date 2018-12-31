using System;

namespace MaiDan.Api.DataContracts.Responses
{
    public class DaySlip
    {
        public DaySlip(Accounting.Domain.DaySlip daySlip)
        {
            Id = daySlip.Id;
            Day = daySlip.Day.Date;
            ClosingDate = daySlip.ClosingDate;
            CashAmount = daySlip.CashAmount;
        }
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public DateTime ClosingDate { get; set; }
        public decimal CashAmount { get; set; }
    }
}
