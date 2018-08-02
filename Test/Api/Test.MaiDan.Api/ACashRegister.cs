using System;
using System.Collections.Generic;
using System.Text;
using MaiDan.Accounting.Domain;
using MaiDan.Api.Services;
using MaiDan.Billing.Domain;
using MaiDan.Infrastructure;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using Moq;
using Test.MaiDan.Billing;
using Dish = MaiDan.Billing.Domain.Dish;

namespace Test.MaiDan.Api
{
    public class ACashRegister
    {
        private IPrint printer = new Mock<IPrint>().Object;
        private IRepository<Dish> menu;
        private IRepository<Order> orderbook;
        private IRepository<Bill> billbook;
        private IRepository<Slip> slipbook;
        private IRepository<Tax> taxConfiguration;
        private IRepository<Discount> discountList;

        public ACashRegister()
        {
        }

        public ACashRegister(IRepository<Dish> menu)
        {
            this.menu = menu;
        }

        public ACashRegister(IRepository<Dish> menu, IRepository<Tax> taxConfiguration)
        {
            this.menu = menu;
            this.taxConfiguration = taxConfiguration;
        }

        public CashRegister Build()
        {
            if (menu == null)
                menu = new Mock<IRepository<Dish>>().Object;

            if (orderbook == null)
                orderbook = new Mock<IRepository<Order>>().Object;

            if (billbook == null)
                billbook = new Mock<IRepository<Bill>>().Object;

            if (slipbook == null)
                slipbook = new Mock<IRepository<Slip>>().Object;

            if (taxConfiguration == null)
                taxConfiguration = new ATaxConfiguration().Build();

            if (discountList == null)
                discountList = new ADiscountList().Build();

            return new CashRegister(printer, menu, orderbook, billbook, slipbook, taxConfiguration, discountList);
        }
    }
}
