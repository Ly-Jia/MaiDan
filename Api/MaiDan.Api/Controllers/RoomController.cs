using MaiDan.Infrastructure.Database;
using MaiDan.Ordering.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Api.Controllers
{
    [Route("api/[controller]")]
    public class RoomController : Controller
    {
        private readonly IRepository<Table> room;

        public RoomController(IRepository<Table> room)
        {
            this.room = room;
        }

        [HttpGet]
        public IEnumerable<DataContracts.Responses.Table> Get()
        {
            return room.GetAll().Select(t => new DataContracts.Responses.Table(t.Id));
        }
    }
}
