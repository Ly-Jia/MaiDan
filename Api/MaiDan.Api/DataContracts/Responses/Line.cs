namespace MaiDan.Api.DataContracts.Responses
{
    public class Line
    {
        public Line(Ordering.Domain.Line orderingLine, Billing.Domain.Line billingLine)
        {
            Id = orderingLine.Id;
            Quantity = orderingLine.Quantity;
            DishId = orderingLine.Dish.Id;
            DishName = orderingLine.Dish.Name;
            Amount = billingLine.Amount;
        }

        public int Id { get; set; }
        public int Quantity { get; set; }
        public string DishId { get; set; }
        public string DishName { get; set; }
        public decimal Amount { get; set; }
    }
}
