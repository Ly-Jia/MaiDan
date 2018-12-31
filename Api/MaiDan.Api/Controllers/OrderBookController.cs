using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MaiDan.Accounting.Dal.Repositories;
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
        private readonly ICashRegister cashRegister;
        private readonly Calendar calendar;

        public OrderBookController(IRepository<Order> orderBook, IRepository<Dish> menu, IRepository<Table> room, ICashRegister cashRegister, Calendar calendar)
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
            catch (InvalidOperationException)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return null;
            }

            Response.StatusCode = (int)HttpStatusCode.OK;
            return new DataContracts.Responses.DetailedOrder(order, billpreview);
        }

        [HttpGet]
        public IEnumerable<DataContracts.Responses.Order> Get()
        {
            return orderBook.GetAll().Select(o => new DataContracts.Responses.Order(o, cashRegister.Calculate(o)));
        }

        [HttpPost]
        public int Add([FromBody] DataContracts.Requests.Order contract)
        {
            Order order;
            try
            {
                if (contract.Id != 0)
                {
                    throw new ArgumentException("The contract id of an order to be created must be 0");
                }

                contract.OrderingDate = DateTime.Now;
                order = ModelFromDataContract(contract);
            }
            catch (ArgumentException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return -1;
            }
            return (int)orderBook.Add(order);
        }

        [HttpPut]
        public void Update([FromBody] DataContracts.Requests.Order contract)
        {
            Order order;
            try
            {
                var day = calendar.GetCurrentDay();
                if (day == null)
                {
                    throw new InvalidOperationException("There isn't an open day available");
                }

                if (day.Date != contract.OrderingDate.Date)
                {
                    throw new ArgumentException($"The day {day.Date} should be closed first");
                }

                if (contract.Id <= 0)
                {
                    throw new ArgumentException("The contract id of an order to be updated cannot be 0 or negative");
                }

                if (orderBook.Get(contract.Id).Closed)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }

                order = ModelFromDataContract(contract);
            }
            catch (ArgumentException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }
            catch (InvalidOperationException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }
            orderBook.Update(order);
        }

        private Order ModelFromDataContract(DataContracts.Requests.Order contract)
        {
            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract), "The contract cannot be null");
            }

            if (contract.Lines == null)
            {
                throw new ArgumentException("The contract lines cannot be null");
            }

            if (contract.IsTakeAway && (contract.Table != null || contract.NumberOfGuests > 0))
            {
                throw new ArgumentException("A take away order should not contains on site order information");
            }

            if (!contract.IsTakeAway && (contract.Table == null || contract.NumberOfGuests == 0))
            {
                throw new ArgumentException("An on-site order should contains table and number of guests information");
            }

            List<Line> lines = new List<Line>();

            foreach (var line in contract.Lines)
            {
                if (line.Id <= 0)
                {
                    throw new ArgumentException("The line id cannot be 0 or negative");
                }

                if (line.Quantity <= 0)
                {
                    throw new ArgumentException("The line quantity cannot be 0 or negative");
                }

                if (line.DishId == null)
                {
                    throw new ArgumentException("The line dish id cannot be null");
                }

                var dish = menu.Get(line.DishId);
                if (dish == null)
                {
                    throw new ArgumentException($"The dish {line.DishId} was not found");
                }

                lines.Add(new Line(line.Id, line.Quantity, dish));
            }

            if (contract.IsTakeAway)
            {
                return new TakeAwayOrder(contract.Id, contract.OrderingDate, lines, false);
            }

            Table table = room.Get(contract.Table.Id);

            if (table == null)
            {
                throw new ArgumentException($"The table {contract.Table.Id} was not found");
            }

            if (contract.NumberOfGuests <= 0)
            {
                throw new ArgumentException("The number of guests cannot be 0 or negative");
            }

            return new OnSiteOrder(contract.Id, table, contract.NumberOfGuests, contract.OrderingDate, lines, false);
        }
    }
}
