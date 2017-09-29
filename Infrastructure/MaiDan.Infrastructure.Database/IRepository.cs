namespace MaiDan.Infrastructure.Database
{
    public interface IRepository<T> where T : class
    {
        T Get(string id);
        void Add(T item);
        bool Update(T item);
        bool Contains(string id);
    }
}
