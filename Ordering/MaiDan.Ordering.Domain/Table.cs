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
            if (!(obj is Table other))
                return false;
            return this.Id == other.Id;
        }
    }
}
