using System.Collections.Generic;
using System.Net;
using MaiDan.Api.DataContract.Ordering;
using MaiDan.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;

namespace MaiDan.Api.Controllers
{
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
        private readonly IRepository<Ordering.Domain.Dish> menuWithoutPrice;
        private readonly IRepository<Billing.Domain.Dish> menuWithPrice;

        public MenuController(IRepository<Ordering.Domain.Dish> orderingMenu, IRepository<Billing.Domain.Dish> billingMenu)
        {
            this.menuWithoutPrice = orderingMenu;
            this.menuWithPrice = billingMenu;
        }

        [HttpGet("{id}")]
        public Billing.Domain.Dish Get(string id)
        {
            Billing.Domain.Dish dish;
            dish = menuWithPrice.Get(id);
            
            if (dish == null)
            {
                Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }

            Response.StatusCode = (int)HttpStatusCode.OK;
            return dish;
        }

        [HttpGet]
        public List<Ordering.Domain.Dish> Get()
        {
            return menuWithoutPrice.GetAll();
        }

        [HttpPut]
        public void Add([FromBody] DishDataContract contract)
        {
            menuWithoutPrice.Add(contract.ToDomainObject());
        }

        [HttpPost]
        public void Update([FromBody] DishDataContract contract)
        {
            menuWithoutPrice.Update(contract.ToDomainObject());
        }
    }
}
