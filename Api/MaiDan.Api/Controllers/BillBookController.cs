using MaiDan.Api.Services;
using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Api.Controllers
{
    [Route("api/[controller]")]
    public class BillBookController :  Controller
    {
        private readonly IRepository<Bill> billBook;
        private readonly IRepository<Order> orderBook;
        private readonly ICashRegister cashRegister;

        public BillBookController(IRepository<Bill> billBook, IRepository<Order> orderBook, ICashRegister cashRegister)
        {
            this.billBook = billBook;
            this.orderBook = orderBook;
            this.cashRegister = cashRegister;
        }

        [HttpGet("{id}")]
        public ActionResult<DataContracts.Responses.DetailedBill> Get(int id)
        {
            var bill = billBook.Get(id);

            if (bill == null)
            {
                return NotFound();
            }

            var order = orderBook.Get(id);
            return new DataContracts.Responses.DetailedBill(order, bill);
        }

        [HttpGet]
        public IEnumerable<DataContracts.Responses.Order> Get()
        {
            return billBook.GetAll().Select(b => new DataContracts.Responses.Order(orderBook.Get(b.Id), b));
        }

        [HttpPost]
        public ActionResult Print([FromBody] DataContracts.Requests.Order contract)
        {
            var order = orderBook.Get(contract.Id);

            if (order.Closed)
            {
                return BadRequest();
            }

            cashRegister.Print(order);
            return Ok();
        }
    }
}
