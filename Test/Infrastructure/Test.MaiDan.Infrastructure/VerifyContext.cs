using System.Net;

namespace Test.MaiDan.Infrastructure
{
    public class VerifyContext
    {
        public static void OutgoingResponseIsOK(AMockOfWebOperationContext aMockOfWebOperationContext)
        {
            aMockOfWebOperationContext.OutgoingResponse.VerifySet(
                outgoingResponse => outgoingResponse.StatusCode = HttpStatusCode.OK);
        }

        public static void OutgoingResponseIsKO(AMockOfWebOperationContext aMockOfWebOperationContext, string expectedStatusDescription)
        {
            aMockOfWebOperationContext.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusCode = HttpStatusCode.InternalServerError);
            aMockOfWebOperationContext.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusDescription = expectedStatusDescription);
        }
        
        public static void OutgoingResponseIsKOMissingMandatoryFields(AMockOfWebOperationContext aMockOfWebOperationContext)
        {
            var expectedStatusDescription = "All mandatory fields are not provided";
            aMockOfWebOperationContext.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusCode = HttpStatusCode.PreconditionFailed);
            aMockOfWebOperationContext.OutgoingResponse.VerifySet(outgoingResponse => outgoingResponse.StatusDescription = expectedStatusDescription);
        }
    }
}