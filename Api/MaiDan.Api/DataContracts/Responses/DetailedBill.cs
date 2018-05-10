using System.Collections.Generic;

namespace MaiDan.Api.DataContracts.Responses
{
    public class DetailedBill : DetailedOrder
    {
        public DetailedBill(Ordering.Domain.Order order, Billing.Domain.Bill bill) : base(order, bill)
        {
            Taxes = new List<Tax>();
            foreach (var tax in bill.Taxes)
            {
                Taxes.Add(new Tax(tax.Key.Rate, tax.Value));
            }

            Discounts = new List<Discount>();
            foreach (var discount in bill.Discounts)
            {
                Discounts.Add(new Discount(discount.Key.Id, discount.Key.Rate, discount.Value));
            }
        }

        public IList<Tax> Taxes { get; set; }

        public IList<Discount> Discounts { get; set; }
    }
}
