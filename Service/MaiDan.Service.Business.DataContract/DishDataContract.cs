using System;
using System.Runtime.Serialization;
using MaiDan.Service.Domain;

namespace MaiDan.Service.Business.DataContract
{
    [DataContract]
    public class DishDataContract : IDataContract<Dish>
    {
        [DataMember]
        public String Id { get; set; }

        [DataMember]
        public String Name { get; set; }

        public Dish ToDomainObject()
        {
            if (this.Id == null)
            {
                throw new ArgumentNullException();
            }
            return new Dish(this.Id, this.Name);
        }
    }
}
