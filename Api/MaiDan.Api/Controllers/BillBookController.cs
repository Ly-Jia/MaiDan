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
        public Bill Get(string id)
        {
            return billBook.Get(id);
        }

        [HttpPost]
        public void Print([FromBody] DataContracts.Requests.Order contract)
        {
            var order = orderBook.Get(contract.Id.ToString());
            cashRegister.Print(order);
        }
    }
}
