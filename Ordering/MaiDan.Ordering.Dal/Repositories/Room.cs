using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Dal.Entities;

namespace MaiDan.Ordering.Dal.Repositories
{
    public class Room : Repository<Table, Domain.Table>
    {
        public Room(IDatabase database) : base(database)
        {
        }

        protected override Table EntityFrom(Domain.Table model)
        {
            return new Table{Id = model.Id};
        }

        protected override Domain.Table ModelFrom(Table entity)
        {
            return new Domain.Table(entity.Id);
        }
    }
}
