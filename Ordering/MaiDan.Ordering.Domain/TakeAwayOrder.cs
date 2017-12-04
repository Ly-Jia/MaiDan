using System;
using System.Collections.Generic;
using System.Text;

namespace MaiDan.Ordering.Domain
{
    public class TakeAwayOrder : Order
    {
        public TakeAwayOrder(string id, IList<Line> lines) : base(id, lines)
        {
        }
    }
}
