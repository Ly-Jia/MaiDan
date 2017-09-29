using System;
using System.Net;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.DataContract;
using MaiDan.Ordering.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MaiDan.Ordering.Api.Controllers
{
    [Route("api/[controller]")]
    public class OrderBookController : Controller
    {
        private readonly IRepository<Order> orderBook;

        public OrderBookController(IRepository<Order> orderBook)
        {
            this.orderBook = orderBook;
        }

        [HttpGet("{id}")]
        public Order Get(string id)
        {
            Order order;
            order = orderBook.Get(id);

            if (order == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }

            Response.StatusCode = (int)HttpStatusCode.OK;
            return order;
        }

        [HttpPut]
        public void Add([FromBody] OrderDataContract contract)
        {
            orderBook.Add(contract.ToDomainObject());
        }

        [HttpPost]
        public void Update([FromBody] OrderDataContract contract)
        {
            orderBook.Update(contract.ToDomainObject());
        }
    }
}
