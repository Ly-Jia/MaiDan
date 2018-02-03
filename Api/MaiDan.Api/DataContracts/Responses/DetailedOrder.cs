using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Api.DataContracts.Responses
{
    public class DetailedOrder : Order
    {
        public DetailedOrder(Ordering.Domain.Order order, Billing.Domain.Bill bill) : base(order, bill)
        {
            Lines = order.Lines.Select(l => new Line(l, bill.Lines.First(b => b.Id == l.Id))).ToList();
        }
        
        public IEnumerable<Line> Lines { get; set; }
    }
}
