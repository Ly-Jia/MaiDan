using System.Collections.Generic;

namespace MaiDan.Ordering.Domain
{
    public class Dish 
    {
        public Dish(string id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Code or short name for dish. Convenient to be read faster by the chief
        /// </summary>
        public string Id { get; }


        /// <summary>
        /// Complete name for customers
        /// </summary>
        public string Name { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is Dish other))
                return false;
            return other.Id == this.Id && other.Name == this.Name;
        }
    }
}
