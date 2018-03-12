using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace MaiDan.Ordering.Dal.Entities
{
    [Table("Dish")]
    public class Dish 
    {
        public Dish()
        {
        }

        public Dish(string id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
        
        [ExplicitKey]
        public string Id { get; set; }
        
        public string Name { get; set; }

        public string Type { get; set; }

    }
}
