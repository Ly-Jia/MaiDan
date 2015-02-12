using System;
using System.Collections.Generic;

namespace MaiDan.DAL
{
	/// <summary>
	/// Description of IRepository.
	/// </summary>
	public interface IRepository<T, U>
	{
	    T Get(U id); 
		void Add(T item);
	    void Update(T item);
	    bool Contains(U id);
	}
}
