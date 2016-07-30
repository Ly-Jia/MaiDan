namespace MaiDan.Infrastructure.Contract
{
    /***
     * Code taken from http://pastebin.com/CjUMwhsG & http://www.whatibroke.com/2014/10/22/resources-for-mocking-wcf/
     ***/

    public interface IWebOperationContext
    {
        IIncomingWebRequestContext IncomingRequest { get; }
        IOutgoingWebResponseContext OutgoingResponse { get; }
    }
}
