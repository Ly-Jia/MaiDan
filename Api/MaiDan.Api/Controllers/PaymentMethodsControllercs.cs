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
        private IRepository<PaymentMethod> paymentMethodList;

        public PaymentMethodsController(IRepository<PaymentMethod> paymentMethodList)
        {
            this.paymentMethodList = paymentMethodList;
        }

        [HttpGet]
        public IEnumerable<DataContracts.Responses.PaymentMethod> Get()
        {
            return paymentMethodList.GetAll().Select(p => new DataContracts.Responses.PaymentMethod(p.Id, p.Name));
        }
    }
}