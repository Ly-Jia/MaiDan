using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Api.DataContracts.Responses
{
    public class DetailedDish : Dish
    {
        public DetailedDish(Ordering.Domain.Dish orderingDish, Billing.Domain.Dish billingDish) : base(orderingDish, billingDish)
        {
            PriceConfiguration = billingDish.PriceConfiguration.Select(p => new Price(p.Amount, p.ValidityStartDate, p.ValidityEndDate)).ToList();
        }

        public IEnumerable<Price> PriceConfiguration { get; set; }
    }
}
