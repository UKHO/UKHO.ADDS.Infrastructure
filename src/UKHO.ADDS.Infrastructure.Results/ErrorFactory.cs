using System.Net;
using UKHO.ADDS.Infrastructure.Results.Errors;

namespace UKHO.ADDS.Infrastructure.Results
{
    public sealed class ErrorFactory
    {
        public static IError CreateError(HttpStatusCode statusCode) =>
            statusCode switch
            {
                HttpStatusCode.BadRequest => new BadRequestError(),
                HttpStatusCode.Unauthorized => new UnauthorizedError(),
                HttpStatusCode.ServiceUnavailable => new ServiceUnavailableError(),
                HttpStatusCode.BadGateway => new DownstreamServiceError(),
                HttpStatusCode.NotFound => new NotFoundError(),
                HttpStatusCode.InternalServerError => new InternalServerError(),
                _ => new HttpError(statusCode)
            };
    }
}
