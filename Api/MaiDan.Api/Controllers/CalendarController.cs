using System;
using MaiDan.Accounting.Dal.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MaiDan.Accounting.Domain;
using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using System.Linq;

namespace MaiDan.Api.Controllers
{
    [Route("api/[controller]")]
    public class CalendarController : Controller
    {
        private readonly Calendar calendar;
        private readonly IRepository<Bill> billBook;
        private readonly IRepository<DaySlip> daySlipBook;

        public CalendarController(Calendar calendar, IRepository<Bill> billBook, IRepository<DaySlip> daySlipBook)
        {
            this.calendar = calendar;
            this.billBook = billBook;
            this.daySlipBook = daySlipBook;
        }

        [HttpGet]
        public DataContracts.Responses.Day GetCurrentDay()
        {
            var day = calendar.GetCurrentDay();
            if (day == null)
                return null;

            return new DataContracts.Responses.Day(day);
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
        public void CloseDay(DataContracts.Requests.DaySlip contract)
        {
            var openedBills = billBook.GetAll();
            if (openedBills.Any())
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
