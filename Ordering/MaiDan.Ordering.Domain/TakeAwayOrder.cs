using System.Collections.Generic;

namespace MaiDan.Ordering.Domain
{
    public class TakeAwayOrder : Order
    {
        public TakeAwayOrder(int id, IList<Line> lines) : base(id, lines)
        {
        }
    }
}
