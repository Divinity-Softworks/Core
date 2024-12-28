using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace DivinitySoftworks.Core.Web.Security;

/// <summary>
/// Interface defining methods for authorization.
/// </summary>
public interface IAuthorizeService {
    /// <summary>
    /// Authorizes a user based on the provided token.
    /// </summary>
    /// <param name="token">The token to authorize.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>An AuthorizeResult indicating the authorization status.</returns>
    Task<AuthorizeResult> Authorize(string token, CancellationToken? cancellationToken = null);
}

/// <summary>
/// Service for authorizing users based on tokens.
/// </summary>
public class AuthorizeService : IAuthorizeService {
    readonly IConfigurationManager<OpenIdConnectConfiguration> _configurationManager;
    readonly AuthorizationOptions _authorizationOptions;

    OpenIdConnectConfiguration? openIdConnectConfiguration;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizeService"/> class.
    /// </summary>
    /// <param name="authorizationOptions">The authorization options.</param>
    public AuthorizeService(IOptions<AuthorizationOptions> authorizationOptions) {
        _authorizationOptions = authorizationOptions.Value;
        _configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
            _authorizationOptions.OidcMetadataUrl,
            new OpenIdConnectConfigurationRetriever(),
            new HttpDocumentRetriever());
    }


    /// <inheritdoc/>
    public async Task<AuthorizeResult> Authorize(string token, CancellationToken? cancellationToken = null) {
        try {
            openIdConnectConfiguration ??= await _configurationManager.GetConfigurationAsync(cancellationToken ?? CancellationToken.None);

            return ValidateToken(token);
        }
        catch (Exception exception) {
            return AuthorizeResult.InternalServerError(exception);
        }
    }

    private AuthorizeResult ValidateToken(string token) {
        if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(token["Bearer ".Length..]))
            return AuthorizeResult.Unauthorized("The request is missing a required token.");

        if (!token.StartsWith("Bearer "))
            return AuthorizeResult.Unauthorized("The request contains a malformed token.");

        TokenValidationParameters validationParameters = new() {
            ValidateIssuer = true,
            ValidIssuer = openIdConnectConfiguration!.Issuer,
            ValidateAudience = true,
            ValidAudience = _authorizationOptions.ValidAudience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKeys = openIdConnectConfiguration.SigningKeys
        };

        try {
            new JwtSecurityTokenHandler()
                .ValidateToken(token["Bearer ".Length..], validationParameters, out SecurityToken validatedToken);

            return AuthorizeResult.IsAuthorized(validatedToken);
        }
        catch (Exception exception) {
            return AuthorizeResult.Unauthorized(exception);
        }
    }
}