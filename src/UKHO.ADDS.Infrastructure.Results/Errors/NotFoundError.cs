namespace UKHO.ADDS.Infrastructure.Results.Errors
{
    public sealed class NotFoundError : Error
    {
        public NotFoundError()
            : base("The requested resource was not found", new Dictionary<string, object>())
        {
        }

        public NotFoundError(string resource)
            : base($"The requested resource '{resource}' was not found", new Dictionary<string, object> { { "Resource", resource } })
        {
        }
    }
}
