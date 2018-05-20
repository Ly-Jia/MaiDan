using System;
using System.Collections.Generic;

namespace MaiDan.Ordering.Domain
{
    public class TakeAwayOrder : Order
    {
        public TakeAwayOrder(int id, DateTime orderingDate, IList<Line> lines, bool closed) : base(id, orderingDate, lines, closed)
        {
        }
    }
}
