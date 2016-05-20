using System;
using System.Runtime.Remoting;

namespace MaiDan.Service.Domain
{
    public class Dish 
    {
        /// <summary>
        /// Code or short name for dish. Convenient to be read faster by the chief
        /// </summary>
        public virtual String Id { get; protected set; }

        /// <summary>
        /// Complete name for customers
        /// </summary>
        public virtual String Name { get; protected set; }

        /// <summary>
        /// constructor only used by NHibernate
        /// </summary>
        protected Dish()
        {
            
        }

        public Dish(String id, String name)
        {
            Id = id;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            var dishToCompare = obj as Dish;

            return dishToCompare.Id == this.Id && dishToCompare.Name == this.Name;
        }
    }
}
