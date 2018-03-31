using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public DataContracts.Responses.DetailedDish Get(string id)
        {
            Ordering.Domain.Dish orderingDish;
            Billing.Domain.Dish billingDish;
            orderingDish = menuWithoutPrice.Get(id);
            billingDish = menuWithPrice.Get(id);
            
            if (orderingDish == null || billingDish == null)
            {
                Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }

            Response.StatusCode = (int)HttpStatusCode.OK;
            return new DataContracts.Responses.DetailedDish(orderingDish, billingDish);
        }

        [HttpGet]
        public IEnumerable<DataContracts.Responses.Dish> Get()
        {
            return menuWithoutPrice.GetAll().Select(dish => new DataContracts.Responses.Dish(dish, menuWithPrice.Get(dish.Id)));
        }

        [HttpPut]
        public void Add([FromBody] DataContracts.Requests.Dish contract)
        {
            menuWithoutPrice.Add(contract.ToOrderingDish());
        }

        [HttpPost]
        public void Update([FromBody] DataContracts.Requests.Dish contract)
        {
            menuWithoutPrice.Update(contract.ToOrderingDish());
        }
    }
}
