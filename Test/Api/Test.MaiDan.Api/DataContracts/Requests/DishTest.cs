﻿using System;
using MaiDan.Api.DataContracts.Requests;
using NFluent;
using NUnit.Framework;

namespace Test.MaiDan.Api.DataContracts.Requests
{
    public class DishTest
    {
        [Test]
        public void can_convert_request_to_Dish()
        {
            var id = "Id";
            var name = "Name";

            var dishDataContract = new Dish() { Id = id, Name = name };
            var dish = new global::MaiDan.Ordering.Domain.Dish(id, name);

            var convertedDish = dishDataContract.ToOrderingDish();

            Check.That(convertedDish).Equals(dish);
        }

        [Test]
        public void should_fail_when_id_is_not_provided_in_data_contract()
        {
            var dishDataContract = new Dish() { Name = "Name" };

            Check.ThatCode(() => dishDataContract.ToOrderingDish()).Throws<ArgumentNullException>();
        }
    }
}
