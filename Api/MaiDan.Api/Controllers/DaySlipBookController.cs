using System.Collections.Generic;
using System.Linq;
using System.Net;
using MaiDan.Accounting.Domain;
using MaiDan.Api.Services;
using MaiDan.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;

namespace MaiDan.Api.Controllers
{
    [Route("api/[controller]")]
    public class DaySlipBookController : Controller
    {
        private readonly IRepository<DaySlip> dayslipBook;
        private readonly ICashRegister cashRegister;

        [HttpGet("{id}")]
        public DataContracts.Responses.DaySlip Get(int id)
        {
            var daySlip = dayslipBook.Get(id);

            if (daySlip == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }
            
            Response.StatusCode = (int)HttpStatusCode.OK;
            return new DataContracts.Responses.DaySlip(daySlip);
        }

        [HttpGet]
        public IEnumerable<DataContracts.Responses.DaySlip> Get()
        {
            return dayslipBook.GetAll().Select(daySlip => new DataContracts.Responses.DaySlip(daySlip));
        }

        [HttpPost]
        public void CloseDay([FromBody] DataContracts.Requests.DaySlip contract)
        {
            cashRegister.CloseDay(contract.ToDaySlip());
        }
    }
}
