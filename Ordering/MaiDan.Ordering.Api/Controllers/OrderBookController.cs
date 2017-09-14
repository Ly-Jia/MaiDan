using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaiDan.Ordering.Dal;
using MaiDan.Ordering.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MaiDan.Ordering.Api.Controllers
{
    [Route("api/[controller]")]
    public class OrderBookController : Controller
    {
        private readonly OrderBook orderBook;

        public OrderBookController(OrderBook orderBook)
        {
            this.orderBook = orderBook;
        }

        [HttpGet("{id}")]
        public Order Get(string id)
        {
            throw new NotImplementedException();
        }
    }
}
