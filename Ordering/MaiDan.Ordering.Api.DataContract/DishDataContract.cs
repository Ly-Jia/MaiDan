using System;
using MaiDan.Ordering.Domain;

namespace MaiDan.Ordering.DataContract
{
    public class DishDataContract : IDataContract<Dish>
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

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
