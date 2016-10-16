using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using MaiDan.Infrastructure.Business;
using MaiDan.Infrastructure.Contract;
using MaiDan.Service.Business.DataContract;
using MaiDan.Service.Dal;
using MaiDan.Service.Domain;

namespace MaiDan.Service.Business
{
    [ServiceContract]
    public class Chief : AbstractService
    {
        private IRepository<Dish, string> menu;

        public Chief() : this(new WebOperationContextWrapper(WebOperationContext.Current), new Menu())
        {
        }

        public Chief(IWebOperationContext context, IRepository<Dish, string> menu)
        {
            this.Context = context;
            this.menu = menu;
        }

        [OperationContract]
        [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/AddToMenu/")]
        public void AddToMenu(IDataContract<Dish> contract)
        {
            try
            {
                menu.Add(contract.ToDomainObject());
            }
            catch (ArgumentNullException)
            {
                SetContextOutgoingResponseToKOMissingFields();
            }
        }

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Update/")]
        public void Update(DishDataContract contract)
        {
            try
            {
                menu.Update(contract.ToDomainObject());
            }
            catch (Exception)
            {
                var statusDescription = String.Format("Cannot update dish {0} - {1} (does not exist)", contract.Id,contract.Name);
                this.SetContextOutgoingResponseStatusToKO(statusDescription);
            }

            SetContextOutgoingResponseStatusToOK();
        }
    }
}
