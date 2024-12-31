using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DivinitySoftworks.Core.Net.Mail; 

/// <summary>
/// Custom JSON converter for serializing and deserializing a <see cref="MailAddress"/>.
/// </summary>
public sealed class MailAddressJsonConverter : JsonConverter<MailAddress> {

    /// <summary>
    /// Reads the JSON representation of a <see cref="MailAddress"/> object.
    /// </summary>
    /// <param name="reader">The reader used to read the JSON data.</param>
    /// <param name="typeToConvert">The type being converted.</param>
    /// <param name="options">The options used during the serialization process.</param>
    /// <returns>A <see cref="MailAddress"/> object.</returns>
    public override MailAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        if (reader.TokenType == JsonTokenType.Null)
            return null!;

        string? emailAddress = reader.GetString();
        return emailAddress is null ? null! : new MailAddress(emailAddress);
    }

    /// <summary>
    /// Writes the JSON representation of a <see cref="MailAddress"/> object.
    /// </summary>
    /// <param name="writer">The writer used to write the JSON data.</param>
    /// <param name="value">The <see cref="MailAddress"/> object to serialize.</param>
    /// <param name="options">The options used during the serialization process.</param>
    public override void Write(Utf8JsonWriter writer, MailAddress value, JsonSerializerOptions options) {
        if(value is not null)
            writer.WriteStringValue(value.ToString());
        writer.WriteNullValue();
    }
}