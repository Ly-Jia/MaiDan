using MaiDan.Accounting.Domain;
using MaiDan.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MaiDan.Api.Controllers
{
    [Route("api/[controller]")]
    public class PaymentMethodsController : Controller
    {
        private IRepository<PaymentMethod> paymentMethods;

        public PaymentMethodsController(IRepository<PaymentMethod> paymentMethods)
        {
            this.paymentMethods = paymentMethods;
        }

        [HttpGet]
        public IEnumerable<DataContracts.Responses.PaymentMethod> Get()
        {
            return paymentMethods.GetAll().Select(p => new DataContracts.Responses.PaymentMethod(p.Id, p.Name));
        }
    }
}