using System;
using System.Runtime.Serialization;

namespace MaiDan.Service.Business.DataContract
{
    [DataContract]
    public class DishDataContract
    {
        [DataMember]
        public String Id { get; set; }

        [DataMember]
        public String Name { get; set; }
    }
}
