using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MaiDan.Accounting.Domain;
using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using System.Linq;
using MaiDan.Ordering.Domain;

namespace MaiDan.Api.Controllers
{
    [Route("api/[controller]")]
    public class CalendarController : Controller
    {
        private readonly ICalendar calendar;
        private readonly IRepository<Order> orderBook;
        private readonly IRepository<Bill> billBook;
        private readonly IRepository<DaySlip> daySlipBook;

        public CalendarController(ICalendar calendar, IRepository<Order> orderBook, IRepository<Bill> billBook, IRepository<DaySlip> daySlipBook)
        {
            this.calendar = calendar;
            this.orderBook = orderBook;
            this.billBook = billBook;
            this.daySlipBook = daySlipBook;
        }

        [HttpGet]
        public DateTime? GetCurrentDay()
        {
            var day = calendar.GetCurrentDay();

            return day?.Date;
        }

        [HttpPost]
        public void OpenDay()
        {
            if (calendar.Contains(DateTime.Today) || calendar.GetCurrentDay() != null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            calendar.Add(new Day(DateTime.Today, false));
        }

        [HttpPut]
        public void CloseDay([FromBody] DataContracts.Requests.DaySlip contract)
        {
            var openedOrders = orderBook.GetAll();
            var openedBills = billBook.GetAll();
            if (openedOrders.Any() || openedBills.Any())
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }
            var day = calendar.Get(contract.Day);
            if (day == null || day.Closed)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }
            var daySlip = contract.ToDaySlip(day);
            daySlipBook.Add(daySlip);
            day.Close();
            calendar.Update(day);
        }
    }
}
