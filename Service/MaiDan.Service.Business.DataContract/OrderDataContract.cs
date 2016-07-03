using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MaiDan.Service.Domain;

namespace MaiDan.Service.Business.DataContract
{
    [DataContract]
    public class OrderDataContract
    {
        [DataMember]
        public virtual DateTime Id { get; set; }

        [DataMember]
        public virtual IList<LineDataContract> Lines { get; set; }

        public Order ToOrder()
        {
            return new Order(this.Id, this.Lines.Select(l => l.ToLine()).ToList());
        }
    }
}
