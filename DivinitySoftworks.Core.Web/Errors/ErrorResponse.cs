using System.Net;
using System.Text.Json.Serialization;

namespace DivinitySoftworks.Core.Web.Errors;
/// <summary>
/// Represents an basic error response.
/// </summary>
public sealed record ErrorResponse {

    /// <summary>
    /// Initializes a new instance of the <see cref="InternalServerErrorResponse"/> class.
    /// </summary>
    public ErrorResponse() {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InternalServerErrorResponse"/> class with the specified error code and description.
    /// </summary>
    /// <param name="exception">The exception thrown.</param>
    public ErrorResponse(Exception exception) {
        Error = exception.Message;
        Description = exception.StackTrace ?? null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InternalServerErrorResponse"/> class with the specified status code, error code, and description.
    /// </summary>
    /// <param name="statusCode">The HTTP status code associated with the error response.</param>
    /// <param name="exception">The exception thrown.</param>
    public ErrorResponse(HttpStatusCode statusCode, Exception exception) {
        StatusCode = statusCode;
        Error = exception.Message;
        Description = exception.StackTrace ?? null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InternalServerErrorResponse"/> class with the specified error code and description.
    /// </summary>
    /// <param name="error">The error code indicating the type of error.</param>
    /// <param name="description">A human-readable description of the error.</param>
    public ErrorResponse(string? error, string? description) {
        Error = error;
        Description = description;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InternalServerErrorResponse"/> class with the specified error code and description.
    /// </summary>
    /// <param name="statusCode">The HTTP status code associated with the error response.</param>
    /// <param name="error">The error code indicating the type of error.</param>
    /// <param name="description">A human-readable description of the error.</param>
    public ErrorResponse(HttpStatusCode statusCode, string? error, string? description) {
        StatusCode = statusCode;
        Error = error;
        Description = description;
    }

    /// <summary>
    /// Gets or sets the HTTP status code associated with the error response.
    /// </summary>
    /// <remarks>
    /// This status code helps the client understand the type of HTTP error, such as 400 for bad requests or 401 for unauthorized access.
    /// </remarks>
    [JsonIgnore]
    public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.InternalServerError;

    /// <summary>
    /// Gets or sets the error code indicating the type of error.
    /// </summary>
    /// <remarks>
    /// This is a machine-readable error code, such as "invalid_request" or "invalid_grant".
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("error")]
    public string? Error { get; init; } = default!;

    /// <summary>
    /// Gets or sets a human-readable description of the error.
    /// </summary>
    /// <remarks>
    /// This description provides additional context for the error, intended for display to the user.
    /// </remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("error_description")]
    public string? Description { get; init; }
}
