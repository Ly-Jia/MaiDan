using System.Collections.Generic;

namespace MaiDan.Ordering.Domain
{
    public class TakeAwayOrder : Order
    {
        public TakeAwayOrder(int id, IList<Line> lines, bool closed) : base(id, lines, closed)
        {
        }
    }
}
