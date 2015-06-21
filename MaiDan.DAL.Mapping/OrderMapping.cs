using MaiDan.Domain.Service;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MaiDan.DAL.Mapping
{
    public class OrderMapping : ClassMapping<Order>
    {
        public OrderMapping()
        {
            Table("[Service.Order]");
            Id(x => x.Id, m => m.Generator(Generators.Assigned));
            Bag(x => x.Lines, c =>
            {
                c.Key(k =>
                {
                    k.Column("[OrderId]");
                    k.NotNullable(true);
                });
                c.Cascade(Cascade.All);
                c.Table("[Service.Line]");
            }, r => r.ManyToMany(m => m.Column("[Id]")));
        }
    }
}
