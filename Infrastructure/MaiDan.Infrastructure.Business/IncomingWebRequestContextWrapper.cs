using System;
using System.ServiceModel.Web;
using MaiDan.Infrastructure.Contract;

namespace MaiDan.Infrastructure.Business
{
    /***
     * Code taken from http://pastebin.com/CjUMwhsG & http://www.whatibroke.com/2014/10/22/resources-for-mocking-wcf/
     ***/

    public class IncomingWebRequestContextWrapper : IIncomingWebRequestContext
    {
        private IncomingWebRequestContext context;

        public IncomingWebRequestContextWrapper(IncomingWebRequestContext context)
        {
            this.context = context;
        }
        
        public string Accept
        {
            get { return context.Accept; }
        }

        public long ContentLength
        {
            get { return context.ContentLength; }
        }

        public string ContentType
        {
            get { return context.ContentType; }
        }

        public System.Net.WebHeaderCollection Headers
        {
            get { return context.Headers; }
        }

        public string Method
        {
            get { return context.Method; }
        }

        public UriTemplateMatch UriTemplateMatch
        {
            get
            {
                return context.UriTemplateMatch;
            }
            set
            {
                context.UriTemplateMatch = value;
            }
        }

        public string UserAgent
        {
            get { return context.UserAgent; }
        }
    }
}
