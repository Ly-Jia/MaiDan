using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Api.DataContracts.Responses
{
    public class Slip : Order
    {
        public Slip(Ordering.Domain.Order order, Billing.Domain.Bill bill, Accounting.Domain.Slip slip) : base(order, bill)
        {
            Payments = slip.Payments.Select(p => new Payment(p.Id, p.Method.Id, p.Amount)).ToList();
        }

        public List<Payment> Payments { get; set; }
    }
}
