using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

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
    /// <summary>
    /// Gets the user ID from the validated JWT token's claims.
    /// </summary>
    /// <remarks>
    /// The user ID is extracted from the "sub" claim, which is the standard claim for the subject identifier 
    /// in OpenID Connect, or the <see cref="ClaimTypes.NameIdentifier"/> claim if "sub" is not present.
    /// Returns <c>null</c> if the token has not been validated or if the user ID claim is missing.
    /// </remarks>
    string? UserId { get; }
}

/// <summary>
/// Service for authorizing users based on tokens.
/// </summary>
public class AuthorizeService : IAuthorizeService {
    readonly IConfigurationManager<OpenIdConnectConfiguration> _configurationManager;
    readonly AuthorizationOptions _authorizationOptions;

    OpenIdConnectConfiguration? openIdConnectConfiguration;
    ClaimsPrincipal? claimsPrincipal;

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
    public string? UserId {
        get {
            return (claimsPrincipal?.FindFirst("sub") 
                ?? claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier))?.Value;
        }
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
            claimsPrincipal = new JwtSecurityTokenHandler()
                .ValidateToken(token["Bearer ".Length..], validationParameters, out SecurityToken validatedToken);
            
            return AuthorizeResult.IsAuthorized(validatedToken);
        }
        catch (Exception exception) {
            claimsPrincipal = null;

            return AuthorizeResult.Unauthorized(exception);
        }
    }
}