namespace UKHO.ADDS.Infrastructure.Results.Errors
{
    public sealed class ServiceUnavailableError : Error
    {
        public ServiceUnavailableError()
            : base("The service is unavailable", new Dictionary<string, object>())
        {
        }
    }
}
