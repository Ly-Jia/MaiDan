using MaiDan.Ordering.Domain;
using Newtonsoft.Json;
using System;

namespace MaiDan.Api.DataContracts.Responses
{
    public class Order
    {
        public Order(Ordering.Domain.Order order, Billing.Domain.Bill bill)
        {
            Id = order.Id;
            if (order is OnSiteOrder onSiteOrder)
            {
                TableId = onSiteOrder.Table.Id;
                NumberOfGuests = onSiteOrder.NumberOfGuests;
            }

            OrderingDate = order.OrderingDate;
            Total = bill.Total;
        }

        public int Id { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string TableId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? NumberOfGuests { get; set; }

        public DateTime OrderingDate { get; set; }

        public decimal Total { get; set; }
    }
}
