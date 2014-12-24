using System;
using System.Collections.Generic;

namespace MaiDan.DAL
{
	/// <summary>
	/// Description of IRepository.
	/// </summary>
	public interface IRepository<T>
	{		
		void Add(T item);
	    void Update(T item);
	}
}
