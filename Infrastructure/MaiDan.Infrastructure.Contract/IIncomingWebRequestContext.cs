using System;
using System.Net;

namespace MaiDan.Infrastructure.Contract
{
    /***
     * Code taken from http://pastebin.com/CjUMwhsG & http://www.whatibroke.com/2014/10/22/resources-for-mocking-wcf/
     ***/

    public interface IIncomingWebRequestContext
    {
        string Accept { get; }
        long ContentLength { get; }
        string ContentType { get; }
        WebHeaderCollection Headers { get; }
        string Method { get; }
        UriTemplateMatch UriTemplateMatch { get; set; }
        string UserAgent { get; }
    }

}
