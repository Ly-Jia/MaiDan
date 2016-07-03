using MaiDan.Service.Business.DataContract;
using MaiDan.Service.Domain;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Service.Business.DataContract
{
    [TestFixture]
    public class LineDataContractTest
    {
        [Test]
        public void can_convert_LineDataContract_To_Line()
        {
            var dishId = "D1";
            var quantity = 1;

            var lineDataContract = new LineDataContract() {DishId = dishId, Quantity = quantity};
            var line = new Line(quantity, dishId);

            var convertedLine = lineDataContract.ToLine();

            Check.That(convertedLine).Equals(line);
        }
    }
}
