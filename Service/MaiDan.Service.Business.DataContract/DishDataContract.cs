using System;
using System.Runtime.Serialization;
using MaiDan.Service.Domain;

namespace MaiDan.Service.Business.DataContract
{
    [DataContract]
    public class DishDataContract
    {
        [DataMember]
        public String Id { get; set; }

        [DataMember]
        public String Name { get; set; }

        public Dish ToDish()
        {
            return new Dish(this.Id, this.Name);
        }
    }
}
