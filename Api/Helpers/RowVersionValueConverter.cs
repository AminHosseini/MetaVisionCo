using System.Text.Json.Serialization;
using System.Text.Json;

namespace Api.Helpers;

public class RowVersionValueConverter : JsonConverter<RowVersionValue>
{
    private readonly static JsonConverter<RowVersionValue> s_defaultConverter =
        (JsonConverter<RowVersionValue>)JsonSerializerOptions.Default.GetConverter(typeof(RowVersionValue));

    public override RowVersionValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var data = reader.GetString();
        if (!string.IsNullOrWhiteSpace(data))
            return new RowVersionValue(data!);

        return s_defaultConverter.Read(ref reader, typeToConvert, options);
    }

    public override void Write([NotNull] Utf8JsonWriter writer, RowVersionValue value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
