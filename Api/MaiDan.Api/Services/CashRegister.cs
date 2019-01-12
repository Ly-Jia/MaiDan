using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using MaiDan.Accounting.Domain;
using MaiDan.Infrastructure;
using Line = MaiDan.Billing.Domain.Line;

namespace MaiDan.Api.Services
{
    public class CashRegister : ICashRegister
    {
        private const string ReducedTaxId = "RED";
        private const string RegularTaxId = "REG";
        private const string TakeAwayDiscountId = "À emporter";
        private readonly IPrint printer;
        private readonly IRepository<Billing.Domain.Dish> menu;
        private readonly IRepository<Order> orderBook;
        private readonly IRepository<Bill> billBook;
        private readonly IRepository<Slip> slipBook;
        private readonly IRepository<Tax> taxConfiguration;
        private readonly IRepository<Discount> discountList;
        private readonly IEnumerable<string> regularTaxedProducts = new[] { "Alcool", "Apéritif", "Vin" };
        private readonly Discount takeAwayDiscount;

        //@TODO set the id in config file

        public CashRegister(IPrint printer, IRepository<Billing.Domain.Dish> menu, IRepository<Order> orderBook, IRepository<Bill> billBook, IRepository<Slip> slipBook, IRepository<Tax> taxConfiguration, IRepository<Discount> discountList)
        {
            this.printer = printer;
            this.menu = menu;
            this.orderBook = orderBook;
            this.billBook = billBook;
            this.slipBook = slipBook;
            this.taxConfiguration = taxConfiguration;
            this.discountList = discountList;
            this.takeAwayDiscount = discountList.Get(TakeAwayDiscountId);
        }

        public Bill Calculate(Order order)
        {
            var lines = order.Lines.Select(CalculateLine).ToList();
            var discounts = new Dictionary<Discount, decimal>();
            AddTakeAwayDiscount(discounts, order, lines);
            var total = lines.Sum(l => l.Amount) - discounts.Sum(d => d.Value);
            var billTaxes = CalculateBillTaxes(order, lines, discounts);
            var bill = new Bill(order.Id, DateTime.Now, lines, discounts, total, billTaxes, false);
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
            printer.Print(new Documents.Bill(order, bill));
        }

        public void Pay(Bill bill)
        {
            var slip = new Slip(bill.Id);
            slipBook.Add(slip);

            CloseBillWhenIsPaid(slip, bill);
        }

        public void AddPayments(Slip slip)
        {
            var bill = billBook.Get(slip.Id);
            slipBook.Update(slip);
            
            CloseBillWhenIsPaid(slip, bill);
        }

        private Line CalculateLine(Ordering.Domain.Line line)
        {
            var dish = menu.Get(line.Dish.Id);

            if (dish.CurrentPrice == null)
            {
                throw new InvalidOperationException($"Dish {dish.Id} does not have a current price");
            }

            var amount = line.Free ? 0m : line.Quantity * dish.CurrentPrice.Value;
            var taxId = regularTaxedProducts.Contains(dish.Type) ? RegularTaxId : ReducedTaxId;
            // @TODO: Make taxconfiguration returns the same object instance for a given taxRate
            var tax = taxConfiguration.Get(taxId);
            var billingLine = new Line(line.Id, amount, tax.CurrentRate);
            return billingLine;
        }

        private void AddTakeAwayDiscount(Dictionary<Discount, decimal> discounts, Order order, List<Line> lines)
        {
            if (!(order is TakeAwayOrder))
            {
                return;
            }

            var discountableAmount = lines.Where(l => l.TaxRate.Tax.Id == takeAwayDiscount.ApplicableTax.Id)
                .GroupBy(l => l.TaxRate.Tax.Id)
                .Select(g => new { TaxId = g, Amount = g.Sum(x => x.Amount) })
                .FirstOrDefault();

            if (discountableAmount?.Amount > 0m)
            {
                var discountAmount = discountableAmount.Amount * takeAwayDiscount.Rate;
                discounts.Add(takeAwayDiscount, discountAmount);
            }
        }

        private Dictionary<TaxRate, decimal> CalculateBillTaxes(Order order, List<Line> lines, Dictionary<Discount, decimal> discounts)
        {
            var billAmountsByTax = lines.GroupBy(l => l.TaxRate.Id)
                .Select(g => new {g.First().TaxRate, Amount = g.Sum(x => x.Amount) });

            var billTaxes = new Dictionary<TaxRate, decimal>();
            foreach (var billAmountByTax in billAmountsByTax)
            {
                var discountAmount = 0m;
                if (order is TakeAwayOrder && billAmountByTax.TaxRate.Tax.Id == ReducedTaxId)
                {
                    discounts.TryGetValue(takeAwayDiscount, out discountAmount);
                }

                var taxAmount = CalculateTaxAmount(billAmountByTax.TaxRate, billAmountByTax.Amount - discountAmount);
                billTaxes.Add(billAmountByTax.TaxRate, taxAmount);
            }
            return billTaxes;
        }

        private decimal CalculateTaxAmount(TaxRate taxRate, decimal amount)
        {
            return Math.Round((amount * taxRate.Rate) / (1 + taxRate.Rate), 2);
        }

        private void CloseBillWhenIsPaid(Slip slip, Bill bill)
        {
            if (BillIsPaid(bill, slip))
            {
                bill.Close();
                billBook.Update(bill);
            }
        }

        private bool BillIsPaid(Bill bill, Slip slip)
        {
            return slip.Payments.Sum(p => p.Amount) >= bill.Total;
        }
    }
}
