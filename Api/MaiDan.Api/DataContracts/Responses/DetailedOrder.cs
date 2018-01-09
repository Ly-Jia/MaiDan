using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Api.DataContracts.Responses
{
    public class DetailedOrder : Order
    {
        public DetailedOrder(Ordering.Domain.Order order, Billing.Domain.Bill billpreview) : base(order, billpreview)
        {
            Lines = order.Lines.Select(l => new Line(l, billpreview.Lines.First(b => b.Id == l.Id))).ToList();
        }
        
        public IEnumerable<Line> Lines { get; set; }
    }
}
