using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Api.Services
{
    public class CashRegister : ICashRegister
    {
        private readonly IRepository<Billing.Domain.Dish> menu;
        private readonly IRepository<Order> orderBook;
        private readonly IRepository<Bill> billBook;
        private readonly IRepository<Tax> taxConfiguration;
        private const string REDUCED_TAX_ID = "RED";
        private const string REGULAR_TAX_ID = "REG";
        private readonly IEnumerable<string> regularTaxedProducts = new[] { "Alcool", "Apéritif", "Vin" };

        public CashRegister(IRepository<Billing.Domain.Dish> menu, IRepository<Order> orderBook, IRepository<Bill> billBook, IRepository<Tax> taxConfiguration)
        {
            this.menu = menu;
            this.orderBook = orderBook;
            this.billBook = billBook;
            this.taxConfiguration = taxConfiguration;
        }

        public Bill Calculate(Order order)
        {
            var lines = order.Lines.Select(CalculateLine).ToList();
            var total = lines.Sum(l => l.Amount);
            // @TODO: Make taxconfiguration returns the same object instance for a given taxRate
            var taxes = lines.GroupBy(l => l.TaxRate.Id).Select(g => new { TaxRate = g.First().TaxRate, Amount = g.Sum(x => x.Amount) });
            var billTaxes = new List<BillTax>();
            foreach (var tax in taxes)
            {
                var taxAmount = CalculateTaxAmount(tax.TaxRate, tax.Amount);
                billTaxes.Add(new BillTax(billTaxes.Count + 1, tax.TaxRate, taxAmount));
            }
            var bill = new Bill(order.Id, lines, total, billTaxes);
            return bill;
        }

        public void Print(Order order)
        {
            if (order.Lines.Count == 0)
            {
                throw new InvalidOperationException("Cannot print an order with no lines");
            }

            order.Close();
            orderBook.Update(order);
            var bill = Calculate(order);
            billBook.Add(bill);
        }

        private Billing.Domain.Line CalculateLine(Ordering.Domain.Line line)
        {
            var dish = menu.Get(line.Dish.Id);
            var amount = line.Quantity * dish.CurrentPrice.Value;
            var taxId = regularTaxedProducts.Contains(dish.Type) ? REGULAR_TAX_ID : REDUCED_TAX_ID;
            var tax = taxConfiguration.Get(taxId);
            var billingLine = new Billing.Domain.Line(line.Id, amount, tax.CurrentRate);
            return billingLine;
        }

        private decimal CalculateTaxAmount(TaxRate taxRate, decimal amount)
        {
            return Math.Round((amount * taxRate.Rate) / (1 + taxRate.Rate), 2);
        }
    }
}
