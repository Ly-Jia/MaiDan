using MaiDan.Domain.Service;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MaiDan.DAL.Mapping
{
    public class LineMapping : ClassMapping<Line>
    {
        public LineMapping()
        {
            Table("[Service.Line]");
            Id(x => x.Id, m => m.Generator(Generators.Assigned));
        }
    }
}
