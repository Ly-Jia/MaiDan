using System;
using System.Collections.Generic;
using MaiDan.Ordering.Domain;
using MaiDan.Ordering.DataContract;

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
		/// <param name="dishId"></param>
		/// <returns></returns>
		public AnOrder With(int quantity, string dishId)
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

	    public AnOrder And(int quantity, string dishId)
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

        /// <summary>
        /// Create an OrderDataContract from order
        /// </summary>
        /// <returns></returns>
	    public OrderDataContract ToOrderContract()
        {
            var orderDataContract = new OrderDataContract()
            {
                Id = Id,
                Lines = new List<LineDataContract>()
            };

            foreach (var line in Lines)
            {
                orderDataContract.Lines.Add(new LineDataContract() {Quantity = line.Quantity, DishId = line.DishId });
            }

            return orderDataContract;
        }
	}
}
