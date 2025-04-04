using System.Text.Json;
using System.Text.Json.Serialization;

namespace Profolio.Server.Dto.Article
{
#nullable disable
	public class HackMDNoteDto
    {
        [JsonPropertyName("id")]
        public string NoteID { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }
        [JsonPropertyName("createdAt")]
        [JsonConverter(typeof(MillisecondEpochConverter))] //毫秒時間戳格式
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("createdAtString")]
        public string CreatedAtString => CreatedAt.ToString("yyyy-MM-dd");
        [JsonPropertyName("updatedAt")]
        [JsonConverter(typeof(NullableMillisecondEpochConverter))]
        public DateTime? UpdatedAt { get; set; }
        [JsonPropertyName("updatedAtString")]
        public string UpdatedAtString => UpdatedAt?.ToString("yyyy-MM-dd");
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
    public class MillisecondEpochConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                throw new JsonException($"Cannot convert null value to {nameof(DateTime)}.");
            }

            var milliseconds = reader.GetInt64();
            if (milliseconds < DateTimeOffset.MinValue.ToUnixTimeMilliseconds() || milliseconds > DateTimeOffset.MaxValue.ToUnixTimeMilliseconds())
            {
                throw new ArgumentOutOfRangeException(nameof(milliseconds), "The value is out of range for a valid DateTime.");
            }
            return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).UtcDateTime;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            if (value < DateTimeOffset.MinValue.UtcDateTime || value > DateTimeOffset.MaxValue.UtcDateTime)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "The DateTime value is out of range for serialization.");
            }
            var milliseconds = new DateTimeOffset(value).ToUnixTimeMilliseconds();
            writer.WriteNumberValue(milliseconds);
        }
    }
    public class NullableMillisecondEpochConverter : JsonConverter<DateTime?>
    {
        private readonly MillisecondEpochConverter _innerConverter = new();

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            return _innerConverter.Read(ref reader, typeof(DateTime), options);
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                _innerConverter.Write(writer, value.Value, options);
            }
        }
    }
}