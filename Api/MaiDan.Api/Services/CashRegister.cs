using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Line = MaiDan.Billing.Domain.Line;

namespace MaiDan.Api.Services
{
    public class CashRegister : ICashRegister
    {
        private readonly IRepository<Billing.Domain.Dish> menu;
        private readonly IRepository<Bill> billBook;
        private readonly IRepository<Tax> taxConfiguration;
        private const string REDUCED_TAX_ID = "RED";
        private const string REGULAR_TAX_ID = "REG";
        private readonly IEnumerable<string> regularTaxedProducts = new[] { "Alcool", "Apéritif", "Vin" };
        //@TODO store discount in database, and make a dedicated repository
        private readonly Discount takeAwayDiscount = new Discount("TA10", 0.10m, new Tax(REDUCED_TAX_ID, null));

        public CashRegister(IRepository<Billing.Domain.Dish> menu, IRepository<Bill> billBook, IRepository<Tax> taxConfiguration)
        {
            this.menu = menu;
            this.billBook = billBook;
            this.taxConfiguration = taxConfiguration;
        }

        public Bill Calculate(Order order)
        {
            var lines = order.Lines.Select(CalculateLine).ToList();
            var discounts = new Dictionary<Discount, decimal>();
            AddTakeAwayDiscount(discounts, order, lines);
            var total = lines.Sum(l => l.Amount) - discounts.Sum(d => d.Value);
            var billTaxes = CalculateBillTaxes(order, lines, discounts);
            var bill = new Bill(order.Id, lines, discounts, total, billTaxes);
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

        private Line CalculateLine(Ordering.Domain.Line line)
        {
            var dish = menu.Get(line.Dish.Id);
            var amount = line.Quantity * dish.CurrentPrice.Value;
            var taxId = regularTaxedProducts.Contains(dish.Type) ? REGULAR_TAX_ID : REDUCED_TAX_ID;
            // @TODO: Make taxconfiguration returns the same object instance for a given taxRate
            var tax = taxConfiguration.Get(taxId);
            var billingLine = new Line(line.Id, amount, tax.CurrentRate);
            return billingLine;
        }

        private void AddTakeAwayDiscount(Dictionary<Discount, decimal> discounts, Order order, List<Line> lines)
        {
            var discountableAmount = lines.Where(l => l.TaxRate.Tax.Id == takeAwayDiscount.ApplicableTax.Id)
                .GroupBy(l => l.TaxRate.Tax.Id)
                .Select(g => new { TaxId = g, Amount = g.Sum(x => x.Amount) })
                .FirstOrDefault();

            if (order is TakeAwayOrder && discountableAmount?.Amount > 0m)
            {
                var discountAmount = discountableAmount.Amount * takeAwayDiscount.Rate;
                discounts.Add(takeAwayDiscount, discountAmount);
            }
        }

        private List<BillTax> CalculateBillTaxes(Order order, List<Line> lines, Dictionary<Discount, decimal> discounts)
        {
            var billAmountsByTax = lines.GroupBy(l => l.TaxRate.Id)
                .Select(g => new { TaxRate = g.First().TaxRate, Amount = g.Sum(x => x.Amount) });
            var billTaxes = new List<BillTax>();
            foreach (var billAmountByTax in billAmountsByTax)
            {
                decimal taxAmount;
                if (order is TakeAwayOrder && billAmountByTax.TaxRate.Tax.Id == REDUCED_TAX_ID &&
                    discounts.Any(d => d.Key == takeAwayDiscount))
                    taxAmount = CalculateTaxAmount(billAmountByTax.TaxRate,
                        billAmountByTax.Amount - discounts[takeAwayDiscount]);
                else
                    taxAmount = CalculateTaxAmount(billAmountByTax.TaxRate, billAmountByTax.Amount);

                billTaxes.Add(new BillTax(billTaxes.Count + 1, billAmountByTax.TaxRate, taxAmount));
            }
            return billTaxes;
        }

        private decimal CalculateTaxAmount(TaxRate taxRate, decimal amount)
        {
            return Math.Round((amount * taxRate.Rate) / (100 + taxRate.Rate), 2);
        }
    }
}
