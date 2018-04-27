using System.ComponentModel.DataAnnotations.Schema;

namespace MaiDan.Ordering.Dal.Entities
{
    [Table("Table")]
    public class Table
    {
        public Table(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
