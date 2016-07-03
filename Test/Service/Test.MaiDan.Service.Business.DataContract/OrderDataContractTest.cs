using System.Collections.Generic;
using MaiDan.Service.Business.DataContract;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Service.Business.DataContract
{
    [TestFixture]
    public class OrderDataContractTest
    {
        [Test]
        public void can_convert_OrderDataContract_to_Order()
        {
            var orderDataContract = new OrderDataContract() {Id = AnOrder.DEFAULT_ID, Lines = new List<LineDataContract>()};
            var order = new AnOrder().Build();

            var convertedOrder = orderDataContract.ToOrder();

            Check.That(convertedOrder).Equals(order);
            Check.That(convertedOrder.Lines).ContainsExactly(order.Lines);
        }
    }
}
