using MaiDan.Billing.Domain;
using MaiDan.Infrastructure.Database;
using Moq;

namespace Test.MaiDan.Billing
{
    public class ADiscountList
    {
        private Mock<IRepository<Discount>> discountList;

        public ADiscountList()
        {
            discountList = new Mock<IRepository<Discount>>();
            var discount = new Discount("TA10", 0.10m, new Tax("RED", null));
            discountList.Setup(d => d.Get("TA10")).Returns(discount);
        }

        public IRepository<Discount> Build()
        {
            return discountList.Object;
        }
    }
}
