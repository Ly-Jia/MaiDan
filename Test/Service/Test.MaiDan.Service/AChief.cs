using System;
using MaiDan.Infrastructure.Contract;
using MaiDan.Service.Business;
using MaiDan.Service.Domain;
using Moq;

namespace Test.MaiDan.Service
{
    public class AChief
    {
        public Mock<IRepository<Dish, string>> Menu { get; private set; }

        public AChief()
        {
            Menu = new Mock<IRepository<Dish, string>>();
        }

        public AChief WithEmptyMenu()
        {
            Menu.Setup(m => m.Update(It.IsAny<Dish>())).Throws<InvalidOperationException>();
            return this;
        }

        public Chief Build()
        {
            return new Chief(Menu.Object);
        }
    }
}