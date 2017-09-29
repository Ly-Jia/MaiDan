using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;

namespace MaiDan.Ordering.Dal
{
    public class Menu : Repository<Dish>
    {
        public Menu(IDatabase database) : base(database)
        {
        }
    }
}
