namespace UKHO.ADDS.Infrastructure.Results.Errors
{
    public sealed class InternalServerError : Error
    {
        public InternalServerError()
            : base("An internal server error occurred", new Dictionary<string, object>())
        {
        }
    }
}
