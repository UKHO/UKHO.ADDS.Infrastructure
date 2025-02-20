namespace UKHO.ADDS.Infrastructure.Results.Errors
{
    public sealed class UnauthorizedError : Error
    {
        public UnauthorizedError()
            : base("The request was unauthorized", new Dictionary<string, object>())
        {
        }
    }
}
