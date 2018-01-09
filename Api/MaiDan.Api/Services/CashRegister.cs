using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using System.Linq;

namespace MaiDan.Api.Services
{
    public class CashRegister
    {
        private readonly IRepository<Billing.Domain.Dish> menu;

        public CashRegister(IRepository<Billing.Domain.Dish> menu)
        {
            this.menu = menu;
        }

        public Bill Calculate(Order order)
        {
            var lines = order.Lines.Select(l => new Billing.Domain.Line(l.Id, l.Quantity * menu.Get(l.Dish.Id).CurrentPrice.Value)).ToList();
            var bill =  new Bill(order.Id, lines);
            return bill;
        }
    }
}
