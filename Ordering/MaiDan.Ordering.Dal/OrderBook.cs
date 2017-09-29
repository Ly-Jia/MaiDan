using System;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;

namespace MaiDan.Ordering.Dal
{
	public class OrderBook : Repository<Order>
	{
	   public OrderBook(IDatabase database) : base(database)
	    {
	    }
	}
}
