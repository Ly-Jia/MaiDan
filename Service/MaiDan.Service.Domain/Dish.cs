using System;

namespace MaiDan.Service.Domain
{
    public class Dish 
    {
        public virtual String Id { get; protected set; }

        /// <summary>
        /// constructor only used by NHibernate
        /// </summary>
        protected Dish()
        {
            
        }

        public Dish(String id)
        {
            Id = id;
        }
    }
}
