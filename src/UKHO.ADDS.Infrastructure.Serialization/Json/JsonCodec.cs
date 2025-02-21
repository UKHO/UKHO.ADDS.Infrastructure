using System.Text.Json;
using System.Text.Json.Serialization;

namespace UKHO.ADDS.Infrastructure.Serialization.Json
{
    public sealed class JsonCodec
    {
        private static readonly JsonSerializerOptions _defaultOptions;
        private static readonly JsonSerializerOptions _defaultOptionsNoFormat;

        static JsonCodec()
        {
            _defaultOptions = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };

            _defaultOptionsNoFormat = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
        }

        public static string Encode<T>(T value) => Encode(value, _defaultOptions);

        public static string Encode<T>(T value, JsonSerializerOptions options) => JsonSerializer.Serialize(value, options);

        public static T? Decode<T>(string json) => Decode<T>(json, _defaultOptions);

        public static T? Decode<T>(string json, JsonSerializerOptions options) => JsonSerializer.Deserialize<T>(json, options);

        public static JsonSerializerOptions DefaultOptions => _defaultOptions;

        public static JsonSerializerOptions DefaultOptionsNoFormat => _defaultOptionsNoFormat;
    }
}
