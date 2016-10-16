using MaiDan.Infrastructure.Contract;

namespace MaiDan.Infrastructure.Business
{
    public abstract class AbstractService
    {
        protected IWebOperationContext Context;
        
        protected void SetContextOutgoingResponseStatusToOK()
        {
            Context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
        }

        protected void SetContextOutgoingResponseStatusToKO(string statusDescription)
        {
            Context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            Context.OutgoingResponse.StatusDescription = statusDescription;
        }

        protected void SetContextOutgoingResponseToKOMissingFields()
        {
            Context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.PreconditionFailed;
            Context.OutgoingResponse.StatusDescription = "All mandatory fields are not provided";
        }
    }
}