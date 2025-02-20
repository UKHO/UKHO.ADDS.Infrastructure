namespace UKHO.ADDS.Infrastructure.Results.Errors
{
    public sealed class DuplicateError : Error
    {
        public DuplicateError()
            : base("The requested resource was duplicated", new Dictionary<string, object>())
        {
        }

        public DuplicateError(string resource)
            : base($"The requested resource '{resource}' was duplicated", new Dictionary<string, object> { { "Resource", resource } })
        {
        }
    }
}
