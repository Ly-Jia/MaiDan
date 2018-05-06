using System.Collections.Generic;

namespace MaiDan.Ordering.Domain
{
    public class OnSiteOrder : Order
    {
        public Table Table { get; }
        public int NumberOfGuests { get; }

        public OnSiteOrder(int id, Table table, int numberOfGuests, IList<Line> lines, bool closed) : base(id, lines, closed)
        {
            Table = table;
            NumberOfGuests = numberOfGuests;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is OnSiteOrder other))
                return false;
            return base.Equals(obj) && other.Table.Equals(this.Table) && other.NumberOfGuests == this.NumberOfGuests;
        }
    }
}
