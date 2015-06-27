using System;
using System.Collections.Generic;
using MaiDan.Service.Dal.Mapping;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

namespace MaiDan.Infrastructure
{
    public class Database : IDatabase
    {
        private ISessionFactory _sessionFactory;
        private Configuration _configuration;
        private HbmMapping _mapping;

        public ISession OpenSession()
        {
            //Open and return the nhibernate session
            return SessionFactory.OpenSession();
        }

        public ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    //Create the session factory
                    _sessionFactory = Configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public Configuration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    //Create the nhibernate configuration
                    _configuration = CreateConfiguration();
                }
                return _configuration;
            }
        }

        public HbmMapping Mapping
        {
            get
            {
                if (_mapping == null)
                {
                    //Create the mapping
                    _mapping = CreateMapping();
                }
                return _mapping;
            }
        }

        private Configuration CreateConfiguration()
        {
            var configuration = new Configuration();
            //Loads properties from hibernate.cfg.xml
            configuration.Configure();
            //Loads nhibernate mappings 
            configuration.AddDeserializedMapping(Mapping, null);

            return configuration;
        }

        private HbmMapping CreateMapping()
        {
            var mapper = new ModelMapper();
            //Add the mapping to the model mapper
            mapper.AddMappings(new List<Type>
            {
                typeof(OrderMapping),
                typeof(LineMapping)
            });
          //  mapper.AddMapping(new LineMapping());
            //Create and return a HbmMapping of the model mapping in code
            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }
    }

    public interface IDatabase
    {
        ISession OpenSession();
    }
}
