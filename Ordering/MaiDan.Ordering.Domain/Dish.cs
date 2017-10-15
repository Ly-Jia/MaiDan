using Dapper.Contrib.Extensions;

namespace MaiDan.Ordering.Domain
{
    [Table("Dish")]
    public class Dish 
    {
        /// <summary>
        /// Code or short name for dish. Convenient to be read faster by the chief
        /// </summary>
        [ExplicitKey]
        public string Id { get; set; }

        /// <summary>
        /// Complete name for customers
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// constructor only used by Dapper
        /// </summary>
        protected Dish()
        {
            
        }

        public Dish(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Dish;
            if (other == null)
                return false;
            return other.Id == this.Id;
        }
    }
}
