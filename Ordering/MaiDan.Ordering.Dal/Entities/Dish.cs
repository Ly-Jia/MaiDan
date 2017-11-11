using Dapper.Contrib.Extensions;

namespace MaiDan.Ordering.Dal.Entities
{
    [Table("Dish")]
    public class Dish 
    {
        public Dish()
        {
        }

        public Dish(string id, string name)
        {
            Id = id;
            Name = name;
        }
        
        [ExplicitKey]
        public string Id { get; set; }
        
        public string Name { get; set; }
        
    }
}
