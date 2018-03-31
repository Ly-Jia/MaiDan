using System.Collections.Generic;
using System.Linq;
using Dapper.Contrib.Extensions;

namespace MaiDan.Infrastructure.Database
{
	public abstract class Repository<TEntity, TModel> : IRepository<TModel> where TEntity : class where TModel : class
	{
        private IDatabase database;

        protected abstract TEntity EntityFrom(TModel model);
	    protected abstract TModel ModelFrom(TEntity entity);

        protected Repository(IDatabase database)
	    {
	        this.database = database;
	    }

        public TModel Get(object id)
        {
            TEntity item;
            using (var connection = database.CreateConnection())
            {
                connection.Open();

                item = connection.Get<TEntity>(id);
            }

            return ModelFrom(item);
        }

	    public virtual List<TModel> GetAll()
	    {
            using (var connection = database.CreateConnection())
            {
                connection.Open();
                var entities = connection.GetAll<TEntity>().ToList();
                return entities.Select(ModelFrom).ToList();
            }
	    }

	    public void Add(TModel item)
	    {
            using (var connection = database.CreateConnection())
            {
                connection.Open();

                connection.Insert(EntityFrom(item));
            }
        }

	    public void Update(TModel item)
	    {
            using (var connection = database.CreateConnection())
            {
                connection.Open();

                connection.Update(EntityFrom(item));
            }
        }

	    public bool Contains(object id)
	    {
            return Get(id) != null;
        }
	}
}
