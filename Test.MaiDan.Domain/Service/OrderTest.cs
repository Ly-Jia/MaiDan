using System;
using System.Collections.Generic;
using NUnit.Framework;
using NFluent;
using MaiDan.Domain.Service;

namespace Test.MaiDan.Domain.Service
{
	[TestFixture]
	public class OrderTest
	{
		[Test]
		public void should_be_identifiable_by_the_creation_date()
		{
			var creationDate = new DateTime(2012,12,21);
			var order = new Order(creationDate);
			Check.That(order.Id).Equals(creationDate);
		}
	}
}