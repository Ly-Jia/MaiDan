using System;
using System.Net;
using MaiDan.Infrastructure.Contract;
using Moq;

namespace Test.MaiDan.Infrastructure
{
    /***
     *  Code taken from http://weblogs.asp.net/cibrax/unit-tests-for-wcf
     ***/
    public class AMockOfWebOperationContext : Mock<IWebOperationContext>
    {
        Mock<IIncomingWebRequestContext> requestContextMock = new Mock<IIncomingWebRequestContext>();
        Mock<IOutgoingWebResponseContext> responseContextMock = new Mock<IOutgoingWebResponseContext>();

        public AMockOfWebOperationContext() : base()
        {
            this.SetupGet(webContext => webContext.OutgoingResponse).Returns(responseContextMock.Object);
            this.SetupGet(webContext => webContext.IncomingRequest).Returns(requestContextMock.Object);

            WebHeaderCollection requestHeaders = new WebHeaderCollection();
            WebHeaderCollection responseHeaders = new WebHeaderCollection();
            UriTemplateMatch uriTemplateMatch = new UriTemplateMatch();

            requestContextMock.SetupGet(requestContext => requestContext.Headers).Returns(requestHeaders);
            requestContextMock.SetupGet(requestContext => requestContext.UriTemplateMatch).Returns(uriTemplateMatch);
            responseContextMock.SetupGet(responseContext => responseContext.Headers).Returns(responseHeaders);
        }

        public Mock<IIncomingWebRequestContext> IncomingRequest
        {
            get { return requestContextMock; }
        }

        public Mock<IOutgoingWebResponseContext> OutgoingResponse
        {
            get { return responseContextMock; }
        }
    }
}
