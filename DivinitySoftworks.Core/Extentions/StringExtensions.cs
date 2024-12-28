using System.Text.Json;

namespace DivinitySoftworks.Core.Extentions;

/// <summary>
/// Provides extension methods for the <see cref="string"/> class.
/// </summary>
public static class StringExtensions {
    /// <summary>
    /// Deserializes a JSON string into an object of type <typeparamref name="T"/>. 
    /// If the string is null, empty, or whitespace, or if deserialization fails, 
    /// the method returns <c>default(T)</c>.
    /// </summary>
    /// <typeparam name="T">The type of object to deserialize to. Must be a reference type.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <returns>
    /// An object of type <typeparamref name="T"/> if deserialization is successful; 
    /// otherwise, <c>default(T)</c>.
    /// </returns>
    public static T? DeserializeJson<T>(this string json) where T : class {
        if (string.IsNullOrWhiteSpace(json))
            return default;

        try {
            return JsonSerializer.Deserialize<T>(json);
        }
        catch (JsonException) {
            return default;
        }
    }
}
