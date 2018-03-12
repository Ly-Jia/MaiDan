using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Dal.Entities;

namespace MaiDan.Ordering.Dal.Repositories
{
    public class Menu : Repository<Dish, Domain.Dish>
    {
        public Menu(IDatabase database) : base(database)
        {
        }

        protected override Dish EntityFrom(Domain.Dish model)
        {
            return new Dish(model.Id, model.Name, model.Type);
        }

        protected override Domain.Dish ModelFrom(Dish entity)
        {
            return new Domain.Dish(entity.Id, entity.Name, entity.Type);
        }
        
    }
}
