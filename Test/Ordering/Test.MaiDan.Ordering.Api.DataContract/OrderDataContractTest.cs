﻿using System.Collections.Generic;
using MaiDan.Ordering.DataContract;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Ordering.Api.DataContract
{
    [TestFixture]
    public class OrderDataContractTest
    {
        [Test]
        public void can_convert_OrderDataContract_to_Order()
        {
            var orderDataContract = new OrderDataContract() { Id = AnOrder.DEFAULT_ID, Lines = new List<LineDataContract>() };
            var order = new AnOrder().Build();

            var convertedOrder = orderDataContract.ToDomainObject();

            Check.That(convertedOrder).Equals(order);
            Check.That(convertedOrder.Lines).ContainsExactly(order.Lines);
        }

        [Test]
        public void should_not_fail_when_lines_are_not_provided_in_data_contract()
        {
            var orderDataContract = new OrderDataContract() { Id = AnOrder.DEFAULT_ID };

            var convertedOrder = orderDataContract.ToDomainObject();

            Check.That(convertedOrder.Lines).IsNotNull();
        }
    }
}