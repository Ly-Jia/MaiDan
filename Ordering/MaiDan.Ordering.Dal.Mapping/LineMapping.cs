﻿using MaiDan.Service.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MaiDan.Service.Dal.Mapping
{
    public class LineMapping : ClassMapping<Line>
    {
        public LineMapping()
        {
            Table("[Service.Line]");
            Id(x => x.Id, m => m.Generator(new IdentityGeneratorDef()));
            
        }
    }
}