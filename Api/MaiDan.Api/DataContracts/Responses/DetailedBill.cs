using System.Collections.Generic;
using MaiDan.Billing.Domain;

namespace MaiDan.Api.DataContracts.Responses
{
    public class DetailedBill : DetailedOrder
    {
        public DetailedBill(Ordering.Domain.Order order, Bill bill) : base(order, bill)
        {
            this.Taxes = bill.Taxes;
        }

        public Dictionary<decimal, decimal> Taxes { get; set; }
    }
}
