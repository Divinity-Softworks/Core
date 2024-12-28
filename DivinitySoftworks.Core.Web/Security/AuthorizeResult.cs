using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace DivinitySoftworks.Core.Web.Security;

/// <summary>
/// Represents the result of an authorization attempt.
/// </summary>
public sealed class AuthorizeResult {

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizeResult"/> class with a validated token.
    /// </summary>
    /// <param name="validatedToken">The validated security token.</param>
    private AuthorizeResult(SecurityToken validatedToken) {
        StatusCode = HttpStatusCode.Continue;
        ValidatedToken = validatedToken;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizeResult"/> class with a status code and error message.
    /// </summary>
    /// <param name="statusCode">The HTTP status code.</param>
    /// <param name="error">The error message.</param>
    private AuthorizeResult(HttpStatusCode statusCode, string error) {
        Error = error;
        StatusCode = statusCode;
    }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    public string? Error { get; init; }

    /// <summary>
    /// Gets the detailed error message.
    /// </summary>
    public string? ErrorMessage { get; private set; }

    /// <summary>
    /// Gets the HTTP status code.
    /// </summary>
    public HttpStatusCode? StatusCode { get; init; }

    /// <summary>
    /// Gets the validated security token.
    /// </summary>
    public SecurityToken? ValidatedToken { get; init; }

    /// <summary>
    /// Creates an <see cref="AuthorizeResult"/> for an unauthorized request with a specified exception.
    /// </summary>
    /// <param name="exception">The exception that caused the unauthorized result.</param>
    /// <returns>An instance of <see cref="AuthorizeResult"/> representing an unauthorized request.</returns>
    internal static AuthorizeResult Unauthorized(Exception exception) {
        return new AuthorizeResult(HttpStatusCode.Unauthorized, "invalid_token") {
            ErrorMessage = exception switch {
                SecurityTokenInvalidSignatureException _ => "The token signature is invalid.",
                SecurityTokenExpiredException _ => "The token has expired.",
                SecurityTokenInvalidLifetimeException _ => "The token's lifetime is invalid.",
                SecurityTokenInvalidIssuerException _ => "The token issuer is invalid.",
                SecurityTokenInvalidAudienceException _ => "The token audience is invalid.",
                _ => "The token is invalid.",
            }
        };
    }

    /// <summary>
    /// Creates an <see cref="AuthorizeResult"/> for an unauthorized request with a specified <paramref name="errorMessage"/>.
    /// </summary>
    /// <param name="errorMessage">The detailed error message.</param>
    /// <returns>An instance of <see cref="AuthorizeResult"/> representing an unauthorized request.</returns>
    internal static AuthorizeResult Unauthorized(string errorMessage) {
        return new AuthorizeResult(HttpStatusCode.Unauthorized, "invalid_token") {
            ErrorMessage = errorMessage
        };
    }

    /// <summary>
    /// Creates an <see cref="AuthorizeResult"/> for an authorized request with a validated token.
    /// </summary>
    /// <param name="validatedToken">The validated security token.</param>
    /// <returns>An instance of <see cref="AuthorizeResult"/> representing an authorized request.</returns>
    internal static AuthorizeResult IsAuthorized(SecurityToken validatedToken) {
        return new AuthorizeResult(validatedToken);
    }

    /// <summary>
    /// Creates an <see cref="AuthorizeResult"/> for an invalid request with a specified error message.
    /// </summary>
    /// <param name="errorMessage">The error message describing the invalid request.</param>
    /// <returns>An instance of <see cref="AuthorizeResult"/> representing an invalid request.</returns>
    internal static AuthorizeResult InvalidRequest(string errorMessage) {
        return new(HttpStatusCode.BadRequest, "invalid_request") {
            ErrorMessage = errorMessage
        };
    }

    /// <summary>
    /// Creates an <see cref="AuthorizeResult"/> for an internal server error with a specified exception.
    /// </summary>
    /// <param name="exception">The exception that caused the internal server error.</param>
    /// <returns>An instance of <see cref="AuthorizeResult"/> representing an internal server error.</returns>
    internal static AuthorizeResult InternalServerError(Exception exception) {
        return new(HttpStatusCode.InternalServerError, "internal_server_error") {
            ErrorMessage = exception.Message
        };
    }
}
