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
        private readonly IRepository<Bill> billBook;
        private readonly IRepository<Dish> menu;
        private readonly IRepository<Table> room;
        private readonly ICashRegister cashRegister;

        public OrderBookController(IRepository<Order> orderBook, IRepository<Bill> billBook, IRepository<Dish> menu, IRepository<Table> room, ICashRegister cashRegister)
        {
            this.orderBook = orderBook;
            this.billBook = billBook;
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
                order = ModelFromDataContract(contract);
            }
            catch (ArgumentException)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return -1;
            }
            orderBook.Add(order);
            return order.Id;
        }

        [HttpPut]
        public void Update([FromBody] DataContracts.Requests.Order contract)
        {
            if (billBook.Contains(contract.Id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return;
            }

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
            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract), "The contract cannot be null");
            }

            if (contract.Id <= 0)
            {
                throw new ArgumentException("The contract id cannot be 0 or negative");
            }

            if (contract.Lines == null)
            {
                throw new ArgumentException("The dish lines cannot be null");
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

            if (string.IsNullOrEmpty(contract.TableId))
            {
                return new TakeAwayOrder(contract.Id, lines);
            }

            Table table = room.Get(contract.TableId);

            if (table == null)
            {
                throw new ArgumentException($"The table {contract.TableId} was not found");
            }

            if (contract.NumberOfGuests <= 0)
            {
                throw new ArgumentException("The number of guests cannot be 0 or negative");
            }

            return new OnSiteOrder(contract.Id, table, contract.NumberOfGuests, lines);
        }
    }
}
