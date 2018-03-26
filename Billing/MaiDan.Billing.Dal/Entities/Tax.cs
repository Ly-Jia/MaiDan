using System.Collections.Generic;

namespace MaiDan.Billing.Dal.Entities
{
    public class Tax
    {
        public string Id { get; set; }
        public List<TaxRate> TaxConfiguration { get; set; }
    }
}
