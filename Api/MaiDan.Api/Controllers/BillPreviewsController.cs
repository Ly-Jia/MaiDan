using MaiDan.Api.Services;
using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MaiDan.Api.Controllers
{
    [Route("api/[controller]")]
    public class BillPreviewsController :  Controller
    {
        private readonly CashRegister cashRegister;
        private readonly IRepository<Order> orderBook;

        public BillPreviewsController(CashRegister cashRegister, IRepository<Order> orderBook)
        {
            this.cashRegister = cashRegister;
            this.orderBook = orderBook;
        }

        [HttpGet("{id}")]
        public Bill Get(string id)
        {
            var order = orderBook.Get(id);
            return cashRegister.Calculate(order);
        }
    }
}
