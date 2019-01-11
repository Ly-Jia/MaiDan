using MaiDan.Accounting.Domain;
using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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
        public IActionResult OpenDay()
        {
            var today = DateTime.Today;
            if (calendar.Contains(today))
            {
                return BadRequest("TodayAlreadyOpened");
            }

            if (calendar.HasOpenedDay())
            {
                return BadRequest("AnotherDayAlreadyOpened");
            }

            calendar.Add(new Day(today, false));

            return Ok();
        }

        [HttpPut]
        public IActionResult CloseDay([FromBody] DataContracts.Requests.DaySlip contract)
        {
            var openedOrders = orderBook.GetAll();
            if (openedOrders.Any())
            {
                return BadRequest("OpenedOrdersPending");
            }

            var openedBills = billBook.GetAll();
            if (openedBills.Any())
            {
                return BadRequest("OpenedBillsPending");
            }

            var day = calendar.Get(contract.Day);
            if (day == null)
            {
                return BadRequest("DayNotOpened");
            }

            if (day.Closed)
            {
                return BadRequest("DayAlreadyClosed");
            }

            var daySlip = contract.ToDaySlip(day);
            daySlipBook.Add(daySlip);
            day.Close();
            calendar.Update(day);

            return Ok();
        }
    }
}
