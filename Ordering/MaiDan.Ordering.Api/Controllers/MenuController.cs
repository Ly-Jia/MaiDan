using System;
using System.Net;
using MaiDan.Ordering.Dal;
using MaiDan.Ordering.DataContract;
using MaiDan.Ordering.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MaiDan.Ordering.Api.Controllers
{
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
        private readonly Menu menu;

        public MenuController(Menu menu)
        {
            this.menu = menu;
        }

        [HttpGet("{id}")]
        public Dish Get(string id)
        {
            try
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return menu.Get(id);
            }
            catch (InvalidOperationException)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }

        }

        [HttpPut]
        public void Add([FromBody] DishDataContract contract)
        {
            menu.Add(contract.ToDomainObject());
        }

        [HttpPost]
        public void Update([FromBody] DishDataContract contract)
        {
            try
            {
                menu.Update(contract.ToDomainObject());
            }
            catch (InvalidOperationException)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

            Response.StatusCode = (int)HttpStatusCode.OK;
        }
    }
}
