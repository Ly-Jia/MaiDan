using System;

namespace MaiDan.Api.DataContracts.Responses
{
    public class Day
    {
        public Day (Accounting.Domain.Day day)
        {
            Date = day.Date;
        }

        public DateTime Date { get; set; }
    }
}
