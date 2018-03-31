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
        private readonly CashRegister cashRegister;
        private readonly IRepository<Bill> billBook;
        private readonly IRepository<Order> orderBook;

        public BillBookController(CashRegister cashRegister, IRepository<Order> orderBook, IRepository<Bill> billBook)
        {
            this.cashRegister = cashRegister;
            this.orderBook = orderBook;
            this.billBook = billBook;
        }

        [HttpGet("{id}")]
        public DataContracts.Responses.DetailedBill Get(string id)
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
        public List<DataContracts.Responses.Order> Get()
        {
            return billBook.GetAll().Select(b => new DataContracts.Responses.Order(orderBook.Get(b.Id.ToString()), b))
                .ToList();
        }

        [HttpPost]
        public void Print([FromBody] DataContracts.Requests.Order contract)
        {
            var id = contract.Id.ToString();
            var bill = billBook.Get(id);
            if (bill != null)
            {
                throw new InvalidOperationException($"The bill {id} has already been printed");
            }

            var order = orderBook.Get(id);
            cashRegister.Print(order);
        }
    }
}
