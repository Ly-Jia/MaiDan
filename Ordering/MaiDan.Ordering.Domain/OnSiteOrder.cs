using System.Collections.Generic;

namespace MaiDan.Ordering.Domain
{
    public class OnSiteOrder : Order
    {
        public Table Table { get; }
        public int NumberOfGuests { get; }

        public OnSiteOrder(string id, Table table, int numberOfGuests, IList<Line> lines) : base(id, lines)
        {
            Table = table;
            NumberOfGuests = numberOfGuests;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is OnSiteOrder other))
                return false;
            return other.Table.Equals(this.Table) && other.NumberOfGuests == this.NumberOfGuests;
        }
    }
}
