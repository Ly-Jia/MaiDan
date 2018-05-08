using System.Collections.Generic;

namespace MaiDan.Infrastructure.Database
{
    public interface IRepository<T> where T : class
    {
        T Get(object id);
        IEnumerable<T> GetAll();
        object Add(T item);
        void Update(T item);
        bool Contains(object id);
    }
}
