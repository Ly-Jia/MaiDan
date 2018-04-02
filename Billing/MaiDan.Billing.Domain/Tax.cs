using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Billing.Domain
{
    public class Tax
    {
        public Tax(string id, IList<TaxRate> taxConfiguration)
        {
            Id = id;
            TaxConfiguration = taxConfiguration;
        }

        public string Id { get; }

        public IList<TaxRate> TaxConfiguration { get; }
        
        public decimal? CurrentRate => TaxConfiguration.FirstOrDefault(p => p.ValidityStartDate <= DateTime.Today && p.ValidityEndDate >= DateTime.Today)?.Rate;

    }
}
