using System;
using System.Collections.Generic;
using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using Moq;

namespace Test.MaiDan.Billing
{
    public class ATaxConfiguration
    {
        private Mock<IRepository<Tax>> taxConfiguration;

        public ATaxConfiguration()
        {
            taxConfiguration = new Mock<IRepository<Tax>>();
            var reducedTax = new Tax("RED", new List<TaxRate>());
            var reducedTaxRate = new TaxRate("RED-1", reducedTax, 0.10m, DateTime.MinValue, DateTime.MaxValue); 
            reducedTax.TaxConfiguration.Add(reducedTaxRate);
            var regularTax = new Tax("REG", new List<TaxRate>());
            var regularTaxRate = new TaxRate("REG-1", regularTax, 0.20m, DateTime.MinValue, DateTime.MaxValue);
            regularTax.TaxConfiguration.Add(regularTaxRate);
            taxConfiguration.Setup(t => t.Get("RED")).Returns(reducedTax);
            taxConfiguration.Setup(t => t.Get("REG")).Returns(regularTax);
        }

        public IRepository<Tax> Build()
        {
            return taxConfiguration.Object;
        }
    }
}
