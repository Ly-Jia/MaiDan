using System;
using MaiDan.Api.DataContract.Ordering;
using MaiDan.Ordering.Domain;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Api.DataContracts.Ordering
{
    [TestFixture]
    public class DishDataContractTest
    {
        [Test]
        public void can_convert_DishDataContract_to_Dish()
        {
            var id = "Id";
            var name = "Name";

            var dishDataContract = new DishDataContract() { Id = id, Name = name };
            var dish = new Dish(id, name);

            var convertedDish = dishDataContract.ToDomainObject();

            Check.That(convertedDish).Equals(dish);
        }

        [Test]
        public void should_fail_when_id_is_not_provided_in_data_contract()
        {
            var dishDataContract = new DishDataContract() { Name = "Name" };

            Check.ThatCode(() => dishDataContract.ToDomainObject()).Throws<ArgumentNullException>();
        }
    }
}
