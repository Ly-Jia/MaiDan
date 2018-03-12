using MaiDan.Ordering.Domain;

namespace Test.MaiDan.Ordering
{
    public class ADish
    {
        private static readonly string DEFAULT_ID = "1";
        private static readonly string DEFAULT_NAME = "Dish 1";
        private static readonly string DEFAULT_TYPE = "Starter";
        private string id;
        private string name;

        public ADish() : this(DEFAULT_ID)
        {
            
        }

        public ADish(string id)
        {
            this.id = id;
        }

        public Dish Build()
        {
            return new Dish(id, name ?? DEFAULT_NAME, DEFAULT_TYPE);
        }

        public ADish Named(string name)
        {
            this.name = name;
            return this;
        }

    }
}
