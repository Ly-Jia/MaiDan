﻿using System;
using System.Collections.Generic;
using MaiDan.Domain.Service;
using MaiDan.DAL;

namespace MaiDan.Business
{
	/// <summary>
	/// Description of Waiter.
	/// </summary>
	public class Waiter
	{
		public IRepository<Order> OrderBook;
		
		public Waiter(IRepository<Order> orderBook)
		{
			OrderBook = orderBook;
		}

		public void Take(Order order)
		{
			OrderBook.Add(order);
		}
	}
	
	
}
