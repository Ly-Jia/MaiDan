using MaiDan.Ordering.Domain;

namespace Test.MaiDan.Ordering
{
    public class ADish
    {
        private const string DefaultId = "1";
        private const string DefaultName = "Dish 1";
        private readonly string id;
        private string name;

        public ADish() : this(DefaultId)
        {
            
        }

        public ADish(string id)
        {
            this.id = id;
        }

        public Dish Build()
        {
            return new Dish(id, name ?? DefaultName);
        }

        public ADish Named(string name)
        {
            this.name = name;
            return this;
        }
    }
}
