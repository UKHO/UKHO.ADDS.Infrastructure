using System.Net;
using UKHO.ADDS.Infrastructure.Results.Errors.Http;
using Xunit;

namespace UKHO.ADDS.Infrastructure.Results.Tests
{
    public class ErrorFactoryTests
    {
        [Theory]
        [InlineData(HttpStatusCode.BadRequest, typeof(BadRequestHttpError))]
        [InlineData(HttpStatusCode.BadGateway, typeof(DownstreamServiceHttpError))]
        [InlineData(HttpStatusCode.InternalServerError, typeof(InternalServerHttpError))]
        [InlineData(HttpStatusCode.NotFound, typeof(NotFoundHttpError))]
        [InlineData(HttpStatusCode.ServiceUnavailable, typeof(ServiceUnavailableHttpError))]
        [InlineData(HttpStatusCode.Unauthorized, typeof(UnauthorizedHttpError))]
        [InlineData(HttpStatusCode.HttpVersionNotSupported, typeof(HttpError))]

        public void ErrorFactory_ShouldCreateCorrectErrorType(HttpStatusCode statusCode, Type expectedType)
        {
            var error = ErrorFactory.CreateError(statusCode);
            Assert.IsType(expectedType, error);
        }

        [Fact]
        public void ErrorFactory_ShouldMergeProvidedProperties()
        {
            var errorProps = new Dictionary<string, object>() { { "CorrelationId", "12345" } };
            var error = ErrorFactory.CreateError(HttpStatusCode.BadRequest, errorProps);

            Assert.Contains("CorrelationId", error.Metadata);
            Assert.Contains("StatusCode", error.Metadata);
        }
    }
}
