using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MaiDan.Api.Services;
using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using Microsoft.AspNetCore.Mvc;

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
        public DataContracts.Responses.DetailedBill Get(int id)
        {
            var bill = billBook.Get(id);

            if (bill == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }

            var order = orderBook.Get(id);
            Response.StatusCode = (int)HttpStatusCode.OK;
            return new DataContracts.Responses.DetailedBill(order, bill);
        }

        [HttpGet]
        public IEnumerable<DataContracts.Responses.Order> Get()
        {
            return billBook.GetAll().Select(b => new DataContracts.Responses.Order(orderBook.Get(b.Id), b));
        }

        [HttpPost]
        public void Print([FromBody] DataContracts.Requests.Order contract)
        {
            if (billBook.Contains(contract.Id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

            var order = orderBook.Get(contract.Id);
            cashRegister.Print(order);
        }
    }
}
