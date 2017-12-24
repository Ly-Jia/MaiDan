using Dapper.Contrib.Extensions;

namespace MaiDan.Ordering.Dal.Entities
{
    [Table("Table")]
    public class Table
    {
        [ExplicitKey]
        public string Id { get; set; }
    }
}
