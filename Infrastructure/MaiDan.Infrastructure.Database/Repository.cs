using System;
using System.Collections.Generic;
using System.Linq;
using Dapper.Contrib.Extensions;

namespace MaiDan.Infrastructure.Database
{
	public abstract class Repository<T> : IRepository<T> where T : class
	{
        private IDatabase database;

	    protected Repository(IDatabase database)
	    {
	        this.database = database;
	    }

        public T Get(string id)
        {
            T item;
            using (var connection = database.CreateConnection())
            {
                connection.Open();

                item = connection.Get<T>(id);
            }

            return item;
        }

	    public virtual List<T> GetAll()
	    {
            using (var connection = database.CreateConnection())
            {
                connection.Open();
                return connection.GetAll<T>().ToList();
            }
	    }

	    public void Add(T item)
	    {
            using (var connection = database.CreateConnection())
            {
                connection.Open();

                connection.Insert(item);
            }
        }

	    public bool Update(T item)
	    {
            using (var connection = database.CreateConnection())
            {
                connection.Open();

                var result = connection.Update(item);
                return result;
            }
        }

	    public bool Contains(string id)
	    {
            return Get(id) != null;
        }
	}
}
