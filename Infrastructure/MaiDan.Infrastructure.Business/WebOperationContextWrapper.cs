using System.ServiceModel.Web;
using MaiDan.Infrastructure.Contract;

namespace MaiDan.Infrastructure.Business
{
    /***
     * Code taken from http://pastebin.com/CjUMwhsG & http://www.whatibroke.com/2014/10/22/resources-for-mocking-wcf/
     ***/

    public class WebOperationContextWrapper : IWebOperationContext
    {
        private WebOperationContext context;

        public WebOperationContextWrapper(WebOperationContext context)
        {
            this.context = context;
        }
        
        public IIncomingWebRequestContext IncomingRequest
        {
            get { return new IncomingWebRequestContextWrapper(context.IncomingRequest); }
        }

        public IOutgoingWebResponseContext OutgoingResponse
        {
            get { return new OutgoingWebResponseContextWrapper(context.OutgoingResponse); }
        }
    }
}
