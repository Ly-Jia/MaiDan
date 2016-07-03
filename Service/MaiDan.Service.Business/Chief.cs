﻿using System;
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
        private IRepository<Dish, string> menu;

        public Chief(IRepository<Dish, string> menu)
        {
            this.menu = menu;
        }

        [OperationContract]
        [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/AddToMenu/")]
        public void AddToMenu(DishDataContract contract)
        {
            menu.Add(contract.ToDish());
        }

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Update/")]
        public void Update(DishDataContract contract)
        {
            try
            {
                menu.Update(contract.ToDish());
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Cannot update dish {contract.Id} - {contract.Name} (does not exist)", e);
            }
            
        }
    }
}
