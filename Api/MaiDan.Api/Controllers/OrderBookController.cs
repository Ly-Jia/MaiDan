using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MaiDan.Api.Services;
using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using Microsoft.AspNetCore.Mvc;
using Dish = MaiDan.Ordering.Domain.Dish;
using Line = MaiDan.Ordering.Domain.Line;

namespace MaiDan.Api.Controllers
{
    [Route("api/[controller]")]
    public class OrderBookController : Controller
    {
        private readonly IRepository<Order> orderBook;
        private readonly IRepository<Dish> menu;
        private readonly IRepository<Table> room;
        private readonly CashRegister cashRegister;

        public OrderBookController(IRepository<Order> orderBook, IRepository<Dish> menu, IRepository<Table> room, CashRegister cashRegister)
        {
            this.orderBook = orderBook;
            this.menu = menu;
            this.room = room;
            this.cashRegister = cashRegister;
        }

        [HttpGet("{id}")]
        public DataContracts.Responses.DetailedOrder Get(int id)
        {
            var order = orderBook.Get(id);

            if (order == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }

            Bill billpreview = null;
            try
            {
                billpreview = cashRegister.Calculate(order);
            }
            catch (InvalidOperationException e)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return null;
            }

            Response.StatusCode = (int)HttpStatusCode.OK;
            return new DataContracts.Responses.DetailedOrder(order, billpreview);
        }

        [HttpGet]
        public List<DataContracts.Responses.Order> Get()
        {
            return orderBook.GetAll().Select(o => new DataContracts.Responses.Order(o, cashRegister.Calculate(o)))
                .ToList();
        }

        [HttpPut]
        public void Add([FromBody] DataContracts.Requests.Order contract)
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
        public void Update([FromBody] DataContracts.Requests.Order contract)
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

        private Order ModelFromDataContract(DataContracts.Requests.Order contract)
        {
            List<Line> lines = new List<Line>();
            if (contract.Lines != null)
                foreach (var line in contract.Lines)
                {
                    var dish = menu.Get(line.DishId);
                    if (dish == null)
                    {
                        throw new ArgumentException($"The dish {line.DishId} was not found");
                    }
                    lines.Add(new Line(line.Id, line.Quantity, dish));
                }

            if (string.IsNullOrEmpty(contract.TableId))
                return new TakeAwayOrder(contract.Id, lines);

            Table table = room.Get(contract.TableId);

            if (table == null)
                throw new ArgumentException($"The table {contract.TableId} was not found");

            return new OnSiteOrder(contract.Id, table, contract.NumberOfGuests, lines);
        }
    }
}
