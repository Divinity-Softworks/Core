using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DivinitySoftworks.Core.Net.Mail;

/// <summary>
/// Custom JSON converter for serializing and deserializing a list of <see cref="MailAddress"/>.
/// </summary>
public sealed class MailAddressListJsonConverter : JsonConverter<List<MailAddress>> {

    /// <summary>
    /// Reads the JSON representation of a list of <see cref="MailAddress"/> objects.
    /// </summary>
    /// <param name="reader">The reader used to read the JSON data.</param>
    /// <param name="typeToConvert">The type being converted.</param>
    /// <param name="options">The options used during the serialization process.</param>
    /// <returns>A list of <see cref="MailAddress"/> objects.</returns>
    public override List<MailAddress> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        List<MailAddress> list = [];

        if (reader.TokenType == JsonTokenType.Null || reader.TokenType != JsonTokenType.StartArray)
            return list;

        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            if (reader.TokenType == JsonTokenType.String) {
                string? emailAddress = reader.GetString();
                if (emailAddress is not null)
                    list.Add(new MailAddress(emailAddress));
            }

        return list;
    }

    /// <summary>
    /// Writes the JSON representation of a list of <see cref="MailAddress"/> objects.
    /// </summary>
    /// <param name="writer">The writer used to write the JSON data.</param>
    /// <param name="value">The list of <see cref="MailAddress"/> to serialize.</param>
    /// <param name="options">The options used during the serialization process.</param>
    public override void Write(Utf8JsonWriter writer, List<MailAddress> value, JsonSerializerOptions options) {
        writer.WriteStartArray();

        foreach (MailAddress address in value)
            if (address is not null) writer.WriteStringValue(address.ToString());

        writer.WriteEndArray();
    }
}