namespace MaiDan.Infrastructure.Contract
{
	public interface IRepository<T, U>
	{
	    T Get(U id); 
		void Add(T item);
	    void Update(T item);
	    bool Contains(U id);
	}
}
