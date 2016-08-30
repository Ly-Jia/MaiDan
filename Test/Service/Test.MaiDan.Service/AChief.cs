using System;
using MaiDan.Infrastructure.Contract;
using MaiDan.Service.Business;
using MaiDan.Service.Domain;
using Moq;
using Test.MaiDan.Infrastructure;

namespace Test.MaiDan.Service
{
    public class AChief
    {
        public Mock<IRepository<Dish, string>> Menu { get; private set; }
        public AMockOfWebOperationContext Context;

        public AChief()
        {
            Menu = new Mock<IRepository<Dish, string>>();
            Context = new AMockOfWebOperationContext();
        }

        public AChief WithEmptyMenu()
        {
            Menu.Setup(m => m.Update(It.IsAny<Dish>())).Throws<InvalidOperationException>();
            return this;
        }

        public Chief Build()
        {
            return new Chief(Context.Object, Menu.Object);
        }
    }
}