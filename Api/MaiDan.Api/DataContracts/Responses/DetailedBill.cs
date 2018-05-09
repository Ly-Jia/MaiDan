using System.Collections.Generic;
using MaiDan.Billing.Domain;

namespace MaiDan.Api.DataContracts.Responses
{
    public class DetailedBill : DetailedOrder
    {
        public DetailedBill(Ordering.Domain.Order order, Bill bill) : base(order, bill)
        {
            Taxes = new List<Tax>();
            foreach (var tax in bill.Taxes)
            {
                Taxes.Add(new Tax(tax.Key.Rate, tax.Value));
            }
        }

        public IList<Tax> Taxes { get; set; }
    }
}
