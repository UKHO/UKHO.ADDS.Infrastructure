using System.Collections;

namespace UKHO.ADDS.Infrastructure.Pipelines.Extensions
{
    internal static class ListExtensions
    {
        public static bool SafeAny(this IList list) => list is { Count: > 0 };
    }
}
