using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;

namespace MaiDan.Ordering.Dal
{
    public class Menu : Repository<Dish, string>
    {
        public Menu(IDatabase database) : base(database)
        {
        }
    }
}
