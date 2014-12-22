using System;
using System.Collections.Generic;
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
		public List<Line> Lines;
		
		/// <summary>
		/// Initialize the future order with a specific Id (creation date)
		/// </summary>
		/// <param name="creationDate"></param>
		public AnOrder(int year, int month, int day)
		{
			Id = new DateTime(year, month, day);
			Lines = new List<Line>();
		}
		
		/// <summary>
		/// Initialize the order with a default Id (DateTime : 2012/12/21)
		/// </summary>
		public AnOrder()
		{
			Id = DEFAULT_ID;
			Lines = new List<Line>();
		}
		
		/// <summary>
		/// Add a new line to the created order
		/// </summary>
		/// <param name="quantity"></param>
		/// <param name="dishName"></param>
		/// <returns></returns>
		public AnOrder With(int quantity, String dishName)
		{
			return With(new Line (quantity, dishName));
		}
		
		/// <summary>
		/// Add a new line to the created order
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public AnOrder With(Line line)
		{
			Lines.Add(line);
			return this;
		}
		
		/// <summary>
		/// Instanciate the order
		/// </summary>
		/// <returns></returns>
		public Order Build()
		{
			return new Order(Id)
			{
				Lines = this.Lines
			};
		}
	}
}
