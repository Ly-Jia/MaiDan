﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Accounting.Dal.Entities
{
    [Table("DaySlip")]
    public class DaySlip
    {
        public DaySlip()
        { }

        public DaySlip(int id, Day day, DateTime closingDate, decimal cashAmount)
        {
            Id = id;
            Day = day;
            ClosingDate = closingDate;
            CashAmount = cashAmount;
        }

        public int Id { get; set; }
        public Day Day { get; set; }
        public DateTime ClosingDate { get; set; }
        public decimal CashAmount { get; set; }
    }
}
