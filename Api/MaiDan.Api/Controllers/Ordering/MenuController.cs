using System.Collections.Generic;
using System.Net;
using MaiDan.Api.DataContract.Ordering;
using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MaiDan.Api.Controllers.Ordering
{
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
        private readonly IRepository<Dish> menu;

        public MenuController(IRepository<Dish> menu)
        {
            this.menu = menu;
        }

        [HttpGet("{id}")]
        public Dish Get(string id)
        {
            Dish dish;
            dish = menu.Get(id);
            
            if (dish == null)
            {
                Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }

            Response.StatusCode = (int)HttpStatusCode.OK;
            return dish;
        }

        [HttpGet]
        public List<Dish> Get()
        {
            return menu.GetAll();
        }

        [HttpPut]
        public void Add([FromBody] DishDataContract contract)
        {
            menu.Add(contract.ToDomainObject());
        }

        [HttpPost]
        public void Update([FromBody] DishDataContract contract)
        {
            menu.Update(contract.ToDomainObject());
        }
    }
}
