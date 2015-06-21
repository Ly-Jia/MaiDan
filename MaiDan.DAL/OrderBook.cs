using System;
using System.Collections.Generic;
using System.Linq;
using MaiDan.Domain.Service;
using MaiDan.Infrastructure;

namespace MaiDan.DAL
{
	/// <summary>
	/// Description of OrderBook.
	/// </summary>
	public class OrderBook : IRepository<Order, DateTime>
	{
	    private IDataBase database;
		/// <summary>
        /// Constructor only for test in OrderBookTest
        /// </summary>
	    public OrderBook()
	    {
	        
	    }

		public OrderBook(IDataBase _database)
		{
            database = _database;
		}
		
		public void Add(Order order)
		{
		    using (var session = database.OpenSession())
		    {
		        session.Transaction.Begin();
		        session.Save(order);
		        session.Transaction.Commit();
		    }
		}

	    public virtual Order Get(DateTime orderId)
	    {
	        Order order;
	        using (var session = database.OpenSession())
	        {
	            order = session.Get<Order>(orderId);
	        }

	        if (order == null)
	        {
                throw new InvalidOperationException("Order " + orderId + "was not found");
	        }
	        return order;
	    }

	    public void Update(Order item)
	    {
	        try
	        {
	            var orderToUpdate = Get(item.Id);
	            orderToUpdate.Update(item.Lines);
	        }
	        catch (Exception e)
	        {
                throw new InvalidOperationException("Cannot update order : " + item.Id, e);
	        }
	    }

	    public bool Contains(DateTime id)
	    {
	        throw new NotImplementedException();
	    }
	}
}
