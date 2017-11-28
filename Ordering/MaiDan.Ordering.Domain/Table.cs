namespace MaiDan.Ordering.Domain
{
    public class Table
    {
        public Table(string id)
        {
            Id = id;
        }

        public string Id { get; }

        public override bool Equals(object obj)
        {
            Table other = obj as Table;
            if (other == null)
                return false;
            return this.Id == other.Id;
        }
    }
}
