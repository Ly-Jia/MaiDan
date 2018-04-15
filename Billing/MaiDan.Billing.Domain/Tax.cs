using System;
using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Billing.Domain
{
    public class Tax
    {
        public Tax(string id, List<TaxRate> taxConfiguration)
        {
            Id = id;
            TaxConfiguration = taxConfiguration;
        }

        public string Id { get; }

        public List<TaxRate> TaxConfiguration { get; }
        
        public TaxRate CurrentRate => TaxConfiguration.FirstOrDefault(p => p.ValidityStartDate <= DateTime.Today && p.ValidityEndDate >= DateTime.Today);

    }
}
