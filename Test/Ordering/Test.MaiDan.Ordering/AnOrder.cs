using System.Collections.Generic;
using MaiDan.Ordering.Domain;

namespace Test.MaiDan.Ordering
{
	/// <summary>
	/// Builder that creates Order
	/// </summary>
	public class AnOrder
	{
		public static readonly string DEFAULT_ID = "id";
		public string Id;
		public List<Line> Lines;
		
		/// <summary>
		/// Initialize the future order with a specific Id (creation date)
		/// </summary>
		/// <param name="id"></param>
		public AnOrder(string id)
		{
            Lines = new List<Line>();
		    Id = id;
		}
		
		/// <summary>
		/// Initialize the order with a default Id (DateTime : 2012/12/21)
		/// </summary>
		public AnOrder():this(DEFAULT_ID)
		{
		}
        
		/// <summary>
		/// Add a new line to the created order
		/// </summary>
		/// <param name="quantity"></param>
		/// <param name="dish"></param>
		/// <returns></returns>
		public AnOrder With(int quantity, Dish dish)
		{
			return With(new Line (quantity, dish));
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
        /// <param name="dish"></param>
        /// <returns></returns>

	    public AnOrder And(int quantity, Dish dish)
	    {
	        return With(quantity, dish); 
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
