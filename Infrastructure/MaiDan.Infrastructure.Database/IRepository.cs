using System.Collections.Generic;

namespace MaiDan.Infrastructure.Database
{
    public interface IRepository<T> where T : class
    {
        T Get(string id);
        List<T> GetAll();
        void Add(T item);
        void Update(T item);
        bool Contains(string id);
    }
}
