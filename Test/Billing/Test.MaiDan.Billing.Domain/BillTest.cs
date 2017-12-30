using System.Collections.Generic;
using MaiDan.Billing.Domain;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Billing.Domain
{
    [TestFixture]
    public class BillTest
    {
        [Test]
        public void should_calculate_total_from_lines_amount()
        {
            var lines = new List<Line>()
                {
                    new Line(1, 1m),
                    new Line(2, 2m)
                };

            var bill = new Bill(1, lines);

            Check.That(bill.Total).Equals(3m);
        }
    }
}
