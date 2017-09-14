using System;
using System.Collections.Generic;
using System.Linq;
using MaiDan.Ordering.Domain;

namespace MaiDan.Ordering.DataContract
{
    public class OrderDataContract
    {
        public virtual DateTime Id { get; set; }
        
        public virtual IList<LineDataContract> Lines { get; set; }

        public Order ToOrder()
        {
            IList<Line> lines;

            if (this.Lines != null)
            {
                lines = this.Lines.Select(l => l.ToLine()).ToList();
            }
            else
            {
                lines = new List<Line>();
            }

            return new Order(this.Id, lines);
        }
    }
}
