using System;
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
        private readonly Tax reducedTax = new Tax("RED", new List<TaxRate>{new TaxRate(10m, new DateTime(2016,01,01), DateTime.MinValue)});
        private readonly Tax regularTax = new Tax("REG", new List<TaxRate>{new TaxRate(20m, new DateTime(2016,01,01), DateTime.MinValue)});
        private readonly IList<string> regularTaxedProducts = new List<string>{ "Alcool", "Apéritif", "Vin"};

        public CashRegister(IRepository<Billing.Domain.Dish> menu, IRepository<Bill> billBook)
        {
            this.menu = menu;
            this.billBook = billBook;
        }

        public Bill Calculate(Order order)
        {
            var lines = order.Lines.Select(l => CalculateLine(l, menu)).ToList();
            var bill =  new Bill(order.Id, lines);
            return bill;
        }

        public void Print(Order order)
        {
            var bill = Calculate(order);
            billBook.Add(bill);
        }

        private Billing.Domain.Line CalculateLine(Ordering.Domain.Line line, IRepository<Billing.Domain.Dish> menu)
        {
            var dish = menu.Get(line.Dish.Id);
            var amount = line.Quantity * dish.CurrentPrice.Value;
            var tax = regularTaxedProducts.Contains(dish.Type) ? regularTax : reducedTax;
            var taxAmount = (amount * 100) / (100 + tax.CurrentRate.Value);
            var billingLine = new Billing.Domain.Line(line.Id, amount, tax, taxAmount);
            return billingLine;
        }
    }
}
