using System;
using System.Collections.Generic;
using System.Net;
using MaiDan.Api.DataContract.Ordering;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MaiDan.Api.Controllers.Ordering
{
    [Route("api/[controller]")]
    public class OrderBookController : Controller
    {
        private readonly IRepository<Order> orderBook;
        private readonly IRepository<Dish> menu;

        public OrderBookController(IRepository<Order> orderBook, IRepository<Dish> menu)
        {
            this.orderBook = orderBook;
            this.menu = menu;
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

        [HttpGet]
        public List<Order> Get()
        {
            return orderBook.GetAll();
        }

        [HttpPut]
        public void Add([FromBody] OrderDataContract contract)
        {
            Order order;
            try
            {
                order = ModelFromDataContract(contract);
            }
            catch (ArgumentException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }
            orderBook.Add(order);
        }

        [HttpPost]
        public void Update([FromBody] OrderDataContract contract)
        {
            Order order;
            try
            {
                order = ModelFromDataContract(contract);
            }
            catch (ArgumentException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }
            orderBook.Update(order);
        }

        private Order ModelFromDataContract(OrderDataContract contract)
        {
            List<Line> lines = new List<Line>();
            if (contract.Lines != null)
                foreach (var line in contract.Lines)
                {
                    var dish = menu.Get(line.DishId);
                    if (dish == null)
                    {
                        throw new ArgumentException();
                    }
                    lines.Add(new Line(line.Quantity, dish));
                }
            Order order = new Order(contract.Id, lines);
            return order;
        }
    }
}
