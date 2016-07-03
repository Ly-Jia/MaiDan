using MaiDan.Service.Business.DataContract;
using MaiDan.Service.Domain;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Service.Business.DataContract
{
    [TestFixture]
    public class DishDataContractTest
    {
        [Test]
        public void can_convert_DishDataContract_to_Dish()
        {
            var id = "Id";
            var name = "Name";

            var dishDataContract = new DishDataContract() {Id = id, Name = name};
            var dish = new Dish(id, name);

            var convertedDish = dishDataContract.ToDish();

            Check.That(convertedDish).Equals(dish);
        }
    }
}
