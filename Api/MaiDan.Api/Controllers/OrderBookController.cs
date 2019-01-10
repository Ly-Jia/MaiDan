using MaiDan.Api.Services;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ICalendar calendar;

        public OrderBookController(IRepository<Order> orderBook, IRepository<Dish> menu, IRepository<Table> room, ICashRegister cashRegister, ICalendar calendar)
        {
            this.orderBook = orderBook;
            this.menu = menu;
            this.room = room;
            this.cashRegister = cashRegister;
            this.calendar = calendar;
        }

        [HttpGet("{id}")]
        public ActionResult<DataContracts.Responses.DetailedOrder> Get(int id)
        {
            var order = orderBook.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            var billpreview = cashRegister.Calculate(order);

            return new DataContracts.Responses.DetailedOrder(order, billpreview);
        }

        [HttpGet]
        public IEnumerable<DataContracts.Responses.Order> Get()
        {
            return orderBook.GetAll().Select(o => new DataContracts.Responses.Order(o, cashRegister.Calculate(o)));
        }

        [HttpPost]
        public IActionResult Add([FromBody] DataContracts.Requests.Order contract)
        {
            var day = calendar.GetCurrentDay();
            if (day == null)
            {
                return BadRequest("There isn't an open day available");
            }

            if (day.Date != DateTime.Today)
            {
                return BadRequest($"The day {day.Date} should be closed first");
            }

            if (contract.Id != 0)
            {
                return BadRequest("The contract id of an order to be created must be 0");
            }

            contract.OrderingDate = DateTime.Now;

            var wrappedOrder = ModelFromDataContract(contract);
            if (wrappedOrder.HasError)
            {
                return BadRequest(wrappedOrder.ErrorMessage);
            }

            return Ok((int)orderBook.Add(wrappedOrder.Model));
        }

        [HttpPut]
        public IActionResult Update([FromBody] DataContracts.Requests.Order contract)
        {
            if (contract.Id <= 0)
            {
                return BadRequest("The contract id of an order to be updated cannot be 0 or negative");
            }

            var orderToUpdate = orderBook.Get(contract.Id);
            if (orderToUpdate.Closed)
            {
                return BadRequest($"The order {contract.Id} is closed");
            }

            var wrappedOrder = ModelFromDataContract(contract, orderToUpdate.OrderingDate);
            if (wrappedOrder.HasError)
            {
                return BadRequest(wrappedOrder.ErrorMessage);
            }

            orderBook.Update(wrappedOrder.Model);

            return Ok();
        }

        // orderingDate overrides with the saved orderingDate, because it shouldn't be changed
        private ValidationResult<Order> ModelFromDataContract(DataContracts.Requests.Order contract, DateTime? orderingDate = null)
        {
            if (contract == null)
            {
                return "The contract cannot be null";
            }

            if (contract.Lines == null)
            {
                return "The contract lines cannot be null";
            }

            if (contract.IsTakeAway && (contract.TableId != null || contract.NumberOfGuests != 0))
            {
                return "A take away order should not contain on-site order information";
            }

            if (!contract.IsTakeAway && (contract.TableId == null || contract.NumberOfGuests <= 0))
            {
                return "An on-site order should contain table and number of guests information";
            }

            var lines = new List<Line>();

            foreach (var line in contract.Lines)
            {
                if (line.Id <= 0)
                {
                    return "The line id cannot be 0 or negative";
                }

                if (line.Quantity <= 0)
                {
                    return "The line quantity cannot be 0 or negative";
                }

                if (line.DishId == null)
                {
                    return "The line dish id cannot be null";
                }

                var dish = menu.Get(line.DishId);
                if (dish == null)
                {
                    return $"The dish {line.DishId} was not found";
                }

                lines.Add(new Line(line.Id, line.Quantity, line.Free, dish));
            }

            var date = orderingDate.HasValue ? orderingDate.Value : contract.OrderingDate;

            if (contract.IsTakeAway)
            {
                return new TakeAwayOrder(contract.Id, date, lines, false);
            }

            var table = room.Get(contract.TableId);

            if (table == null)
            {
                return $"The table {contract.TableId} was not found";
            }

            if (contract.NumberOfGuests <= 0)
            {
                return "The number of guests cannot be 0 or negative";
            }

            return new OnSiteOrder(contract.Id, table, contract.NumberOfGuests, date, lines, false);
        }
    }
}
