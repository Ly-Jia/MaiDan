using System;
using System.Runtime.Serialization;
using MaiDan.Service.Domain;

namespace MaiDan.Service.Business.DataContract
{
    [DataContract]
    public class LineDataContract
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public String DishId { get; set; }

        public Line ToLine()
        {
            return new Line(this.Quantity, this.DishId);
        }
    }
}
