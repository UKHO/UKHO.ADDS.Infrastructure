// ReSharper disable once CheckNamespace
namespace System.Text.Json
{
    public static class JsonSerializerOptionsExtensions
    {
        public static void CopyTo(this JsonSerializerOptions options, JsonSerializerOptions destination, bool clearExistingConverters = false)
        {
            destination.PropertyNamingPolicy = options.PropertyNamingPolicy;
            destination.PropertyNameCaseInsensitive = options.PropertyNameCaseInsensitive;
            destination.WriteIndented = options.WriteIndented;
            destination.DefaultIgnoreCondition = options.DefaultIgnoreCondition;

            if (clearExistingConverters)
            {
                destination.Converters.Clear();
            }

            foreach (var converter in options.Converters)
            {
                destination.Converters.Add(converter);
            }
        }
    }
}
