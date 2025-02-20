using System.Net;

namespace UKHO.ADDS.Infrastructure.Results.Errors
{
    public sealed class HttpError : Error
    {
        public HttpError(HttpStatusCode statusCode)
            : base($"An HTTP error occured, status code {statusCode}", new Dictionary<string, object> { { "StatusCode", statusCode } })
        {
        }

        public HttpStatusCode StatusCode => (HttpStatusCode)Metadata["StatusCode"];
    }
}
