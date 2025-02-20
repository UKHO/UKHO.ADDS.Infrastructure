namespace UKHO.ADDS.Infrastructure.Results.Errors
{
    public sealed class DownstreamServiceError : Error
    {
        public DownstreamServiceError()
            : base("A downstream service error occurred", new Dictionary<string, object>())
        {
        }
    }
}
