using System.Text.Json.Serialization;

namespace DivinitySoftworks.Core.Web.Security;

/// <summary>
/// Represents an interface for objects requiring an API key.
/// </summary>
public interface IApiKey {
    /// <summary>
    /// Gets or sets the value of the API key. 
    /// </summary>
    /// <value>The API key value.</value>
    /// <remarks>
    /// When serializing from JSON, the propery name should be: <c>api_key</c>.
    /// </remarks>
    string Value { get; set; }
}

/// <summary>
/// Represents an API key used for authorization.
/// </summary>
public class ApiKey : IApiKey {

    /// <inheritdoc/>    
    [JsonPropertyName("api_key")]
    public string Value { get; set; } = string.Empty;
}