using System;
using System.Collections.Generic;
using MaiDan.Service.Domain;

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
		public AnOrder(int year, int month, int day) : this(new DateTime(year,month,day))
		{
			
		}
		
		/// <summary>
		/// Initialize the order with a default Id (DateTime : 2012/12/21)
		/// </summary>
		public AnOrder():this(DEFAULT_ID)
		{
		}

        /// <summary>
        /// Initialize the future order with a specific Id (creation date)
        /// </summary>
        /// <param name="creationDate"></param>
	    public AnOrder(DateTime id)
	    {
            Id = id;
            Lines = new List<Line>();
	    }
		
		/// <summary>
		/// Add a new line to the created order
		/// </summary>
		/// <param name="quantity"></param>
		/// <param name="dishId"></param>
		/// <returns></returns>
		public AnOrder With(int quantity, String dishId)
		{
			return With(new Line (quantity, dishId));
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
        /// Add a new line to the created order
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="dishId"></param>
        /// <returns></returns>

	    public AnOrder And(int quantity, String dishId)
	    {
	        return With(quantity, dishId); 
	    }
		
		/// <summary>
		/// Instanciate the order
		/// </summary>
		/// <returns></returns>
		public Order Build()
		{
			return new Order(Id,Lines);
		}
	}
}
