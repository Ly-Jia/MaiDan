﻿using MaiDan.Billing.Domain;
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
            var taxes = lines.GroupBy(l => l.TaxRate).Select(g => new { TaxRate = g.Key, Amount = g.Sum(x => x.Amount)});
            var billTaxes = new List<BillTax>();
            foreach (var tax in taxes)
            {
                var taxAmount = CalculateTaxAmount(tax.TaxRate, tax.Amount);
                billTaxes.Add(new BillTax(billTaxes.Count+1, tax.TaxRate, taxAmount));
            }
            var bill =  new Bill(order.Id, lines, total, billTaxes);
            return bill;
        }

        public void Print(Order order)
        {
            if (order.Lines.Count == 0)
            {
                throw new InvalidOperationException("Cannot print an order with no lines");
            }

            var bill = Calculate(order);
            billBook.Add(bill);
        }

        private Billing.Domain.Line CalculateLine(Ordering.Domain.Line line)
        {
            var dish = menu.Get(line.Dish.Id);
            var amount = line.Quantity * dish.CurrentPrice.Value;
            var tax = regularTaxedProducts.Contains(dish.Type) ? regularTax : reducedTax;
            var taxAmount = CalculateTaxAmount(tax.CurrentRate, amount);
            var billingLine = new Billing.Domain.Line(line.Id, amount, tax.CurrentRate, taxAmount);
            return billingLine;
        }

        private decimal CalculateTaxAmount(TaxRate taxRate, decimal amount)
        {
            return Math.Round((amount * taxRate.Rate) / (100 + taxRate.Rate), 2);
        }
    }
}
