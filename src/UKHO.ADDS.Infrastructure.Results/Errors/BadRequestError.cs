using System.Security.Permissions;

namespace UKHO.ADDS.Infrastructure.Results.Errors
{
    public sealed class BadRequestError : Error
    {
        public BadRequestError()
            : base("The request was bad", new Dictionary<string, object>())
        {
        }
    }
}
