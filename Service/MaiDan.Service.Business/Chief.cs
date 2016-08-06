using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using MaiDan.Infrastructure.Contract;
using MaiDan.Service.Business.DataContract;
using MaiDan.Service.Domain;

namespace MaiDan.Service.Business
{
    [ServiceContract]
    public class Chief
    {
        private IWebOperationContext context;
        private IRepository<Dish, string> menu;

        public Chief(IWebOperationContext context, IRepository<Dish, string> menu)
        {
            this.context = context;
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
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.PreconditionFailed;
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
            catch (Exception e)
            {
                var message = String.Format("Cannot update dish {0} - {1} (does not exist)",contract.Id,contract.Name);
                throw new InvalidOperationException(message, e);
            }
            
        }
    }
}
