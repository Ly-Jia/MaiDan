using MaiDan.Service.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MaiDan.Service.Dal.Mapping
{
    public class DishMapping : ClassMapping<Dish>
    {
        public DishMapping()
        {
            Id(x => x.Id, m => m.Generator(Generators.Assigned));
        }
    }
}
