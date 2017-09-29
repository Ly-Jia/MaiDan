using System;
using System.Collections.Generic;
using System.Linq;
using MaiDan.Ordering.Domain;

namespace MaiDan.Ordering.DataContract
{
    public class OrderDataContract : IDataContract<Order>
    {
        public virtual string Id { get; set; }
        
        public virtual IList<LineDataContract> Lines { get; set; }

        public Order ToDomainObject()
        {
            IList<Line> lines;

            if (this.Lines != null)
            {
                lines = this.Lines.Select(l => l.ToDomainObject()).ToList();
            }
            else
            {
                lines = new List<Line>();
            }

            return new Order(this.Id, lines);
        }
    }
}
