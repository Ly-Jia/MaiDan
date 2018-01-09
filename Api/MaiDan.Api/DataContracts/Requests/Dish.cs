using System;

namespace MaiDan.Api.DataContracts.Requests
{
    public class Dish 
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public Ordering.Domain.Dish ToOrderingDish()
        {
            if (this.Id == null)
            {
                throw new ArgumentNullException();
            }
            return new Ordering.Domain.Dish(this.Id, this.Name);
        }
    }
}
