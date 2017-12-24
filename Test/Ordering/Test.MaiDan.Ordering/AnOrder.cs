using System.Collections.Generic;
using MaiDan.Ordering.Domain;

namespace Test.MaiDan.Ordering
{
	/// <summary>
	/// Builder that creates Order
	/// </summary>
	public class AnOrder
	{
		public static readonly int DEFAULT_ID = 1;
		private int id;
		private List<Line> lines;
	    private bool takeAway = false;
		
		/// <summary>
		/// Initialize the future order with a specific id (creation date)
		/// </summary>
		/// <param name="id"></param>
		public AnOrder(int id)
		{
            this.lines = new List<Line>();
		    this.id = id;
		}
		
		/// <summary>
		/// Initialize the order with a default id (DateTime : 2012/12/21)
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
			return With(new Line (this.lines.Count, quantity, dish));
		}
		
		/// <summary>
		/// Add a new line to the created order
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public AnOrder With(Line line)
		{
			lines.Add(line);
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
			return new TakeAwayOrder(id, lines);
		}
        
	}
}
