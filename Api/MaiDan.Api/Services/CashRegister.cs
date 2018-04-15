using System.Collections.Generic;
using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using System.Linq;

namespace MaiDan.Api.Services
{
    public class CashRegister
    {
        private readonly IRepository<Billing.Domain.Dish> menu;
        private readonly IRepository<Bill> billBook;
        private readonly Tax reducedTax;
        private const string REDUCED_TAX_ID = "RED";
        private readonly Tax regularTax;
        private const string REGULAR_TAX_ID = "REG";
        private readonly IList<string> regularTaxedProducts = new List<string>{ "Alcool", "Apéritif", "Vin"};

        public CashRegister(IRepository<Billing.Domain.Dish> menu, IRepository<Bill> billBook, IRepository<Tax> taxConfiguration)
        {
            this.menu = menu;
            this.billBook = billBook;
            reducedTax = taxConfiguration.Get(REDUCED_TAX_ID);
            regularTax = taxConfiguration.Get(REGULAR_TAX_ID);
        }

        public Bill Calculate(Order order)
        {
            var lines = order.Lines.Select(CalculateLine).ToList();
            var total = lines.Sum(l => l.Amount);
            var taxes = lines.GroupBy(l => l.TaxRate).Select(g => new { TaxRate = g.Key, Amount = g.Sum(x => x.TaxAmount)});
            var billTaxes = new List<BillTax>();
            foreach (var tax in taxes)
            {
                billTaxes.Add(new BillTax(billTaxes.Count+1, tax.TaxRate, tax.Amount));
            }
            var bill =  new Bill(order.Id, lines, total, billTaxes);
            return bill;
        }

        public void Print(Order order)
        {
            var bill = Calculate(order);
            billBook.Add(bill);
        }

        private Billing.Domain.Line CalculateLine(Ordering.Domain.Line line)
        {
            var dish = menu.Get(line.Dish.Id);
            var amount = line.Quantity * dish.CurrentPrice.Value;
            var tax = regularTaxedProducts.Contains(dish.Type) ? regularTax : reducedTax;
            var taxAmount = (amount * 100) / (100 + tax.CurrentRate.Rate);
            var billingLine = new Billing.Domain.Line(line.Id, amount, tax.CurrentRate, taxAmount);
            return billingLine;
        }
    }
}
