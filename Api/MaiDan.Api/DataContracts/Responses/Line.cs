namespace MaiDan.Api.DataContracts.Responses
{
    public class Line
    {
        public Line(Ordering.Domain.Line orderingLine, Billing.Domain.Line billingLine)
        {
            Quantity = orderingLine.Quantity;
            DishName = orderingLine.Dish.Name;
            Amount = billingLine.Amount;
        }

        public int Quantity { get; set; }
        public string DishName { get; set; }
        public decimal Amount { get; set; }
    }
}
