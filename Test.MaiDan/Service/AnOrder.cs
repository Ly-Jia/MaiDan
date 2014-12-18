using System;
using MaiDan.Domain.Service;

namespace Test.MaiDan.Service
{
	/// <summary>
	/// Builder that creates Order
	/// </summary>
	public class AnOrder
	{
		public static readonly DateTime DEFAULT_ID = new DateTime(2012,12,21);
		public DateTime Id;
		
		/// <summary>
		/// Initialize the future order with a specific Id (creation date)
		/// </summary>
		/// <param name="creationDate"></param>
		public AnOrder(DateTime creationDate)
		{
			Id = creationDate;
		}
		
		/// <summary>
		/// Initialize the future order with a default Id (DateTime : 2012/12/21)
		/// </summary>
		public AnOrder()
		{
			Id = DEFAULT_ID;
		}
		
		/// <summary>
		/// Instanciate the order
		/// </summary>
		/// <returns></returns>
		public Order Build()
		{
			return new Order(Id);
		}
	}
}
