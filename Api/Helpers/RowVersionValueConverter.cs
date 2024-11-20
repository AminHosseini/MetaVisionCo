namespace Api.Helpers;

/// <summary>
/// RowVersion تغییر دهنده حالت نمایش
/// </summary>
public class RowVersionValueConverter : JsonConverter<RowVersionValue>
{
    private readonly static JsonConverter<RowVersionValue> s_defaultConverter =
        (JsonConverter<RowVersionValue>)JsonSerializerOptions.Default.GetConverter(typeof(RowVersionValue));

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public override RowVersionValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        var data = reader.GetString();
        if (!string.IsNullOrWhiteSpace(data))
            return new RowVersionValue(data!);

        return s_defaultConverter.Read(ref reader, typeToConvert, options);
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public override void Write([NotNull] Utf8JsonWriter writer, RowVersionValue value, JsonSerializerOptions options)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        writer.WriteStringValue(value.ToString());
    }
}
