using MaiDan.Domain.Service;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MaiDan.DAL.Mapping
{
    public class DishMapping : ClassMapping<Dish>
    {
        public DishMapping()
        {
            Id(x => x.Id, m => m.Generator(Generators.Assigned));
        }
    }
}
