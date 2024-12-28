using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DivinitySoftworks.Core.Web.Http.Identity;

/// <summary>
/// Service to manage OAuth2 tokens, including obtaining and refreshing access tokens from an Identity Server.
/// </summary>
/// <param name="httpClient">The HTTP client to use for making requests to the Identity Server.</param>
/// <param name="settings">The settings for the Identity Server.</param>
/// <param name="logger">The logger instance to use for logging.</param>
public sealed class TokenService(HttpClient httpClient, IOptions<IdentityServerSettings> settings, ILogger<TokenService> logger) {
    readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    readonly ILogger<TokenService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    readonly IdentityServerSettings _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));

    string _accessToken = string.Empty;
    string _refreshToken = string.Empty;
    DateTime _accessTokenExpiresAt = DateTime.MinValue;

    /// <summary>
    /// Gets a valid access token, refreshing it if necessary.
    /// </summary>
    /// <returns>The access token as a string.</returns>
    public async Task<string> GetTokenAsync() {
        if (!string.IsNullOrEmpty(_accessToken) && DateTime.UtcNow < _accessTokenExpiresAt)
            return _accessToken;

        if (!string.IsNullOrEmpty(_refreshToken)) {
            try {
                await RefreshAccessTokenAsync();
            }
            catch (UnauthorizedAccessException) {
                await RequestNewAccessTokenAsync();
            }
        }
        else {
            await RequestNewAccessTokenAsync();
        }

        return _accessToken;
    }

    /// <summary>
    /// Requests a new access token using the client credentials flow.
    /// </summary>
    private async Task RequestNewAccessTokenAsync() {
        DiscoveryDocumentResponse discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync(_settings.Authority);
        if (discoveryDocument.IsError) {
            _logger.LogError("Error retrieving discovery document: {Error}", discoveryDocument.Error);
            throw new InvalidOperationException("Error retrieving discovery document.");
        }

        TokenResponse tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest {
            Address = discoveryDocument.TokenEndpoint,
            ClientId = _settings.ClientId,
            ClientSecret = _settings.ClientSecret,
            GrantType = "client_credentials",
            Scope = _settings.Scope
        });

        if (tokenResponse.IsError || tokenResponse.AccessToken is null || tokenResponse.RefreshToken is null) {
            _logger.LogError("Error retrieving access token: {Error}", tokenResponse.Error);
            throw new InvalidOperationException("Error retrieving access token.");
        }

        _accessToken = tokenResponse.AccessToken;
        _refreshToken = tokenResponse.RefreshToken;
        _accessTokenExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn);

        _logger.LogInformation("Successfully retrieved new access token.");
    }

    /// <summary>
    /// Refreshes the access token using the refresh token.
    /// </summary>
    private async Task RefreshAccessTokenAsync() {
        DiscoveryDocumentResponse discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync(_settings.Authority);
        if (discoveryDocument.IsError) {
            _logger.LogError("Error retrieving discovery document: {Error}", discoveryDocument.Error);
            throw new InvalidOperationException("Error retrieving discovery document.");
        }

        TokenResponse tokenResponse = await _httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest {
            Address = discoveryDocument.TokenEndpoint,
            ClientId = _settings.ClientId,
            ClientSecret = _settings.ClientSecret,
            RefreshToken = _refreshToken
        });

        if (tokenResponse.IsError || tokenResponse.AccessToken is null || tokenResponse.RefreshToken is null) {
            _logger.LogError("Error refreshing access token: {Error}", tokenResponse.Error);
            throw new UnauthorizedAccessException("Error refreshing access token.");
        }

        _accessToken = tokenResponse.AccessToken;
        _refreshToken = tokenResponse.RefreshToken;
        _accessTokenExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn);

        _logger.LogInformation("Successfully refreshed access token.");
    }
}
