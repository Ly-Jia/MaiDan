using System;
using Dapper.Contrib.Extensions;

namespace MaiDan.Infrastructure.Database
{
	public abstract class Repository<T, U> where T : class
	{
        private IDatabase database;

	    protected Repository(IDatabase database)
	    {
	        this.database = database;
	    }

        public T Get(U id)
        {
            T item;
            using (var connection = database.CreateConnection())
            {
                connection.Open();

                item = connection.Get<T>(id);
            }

            return item;
        }

	    public void Add(T item)
	    {
            using (var connection = database.CreateConnection())
            {
                connection.Open();

                connection.Insert(item);
            }
        }

	    public void Update(T item)
	    {
            using (var connection = database.CreateConnection())
            {
                connection.Open();

                var result = connection.Update(item);
                if (!result)
                    throw new InvalidOperationException();
            }
        }

	    public bool Contains(U id)
	    {
            return Get(id) != null;
        }
	}
}
