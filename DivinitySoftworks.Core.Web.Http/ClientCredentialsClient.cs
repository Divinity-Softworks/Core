using DivinitySoftworks.Core.Web.Http.Identity;

namespace DivinitySoftworks.Core.Web.Http;

/// <summary>
/// Represents a client for making HTTP requests using client credentials for authorization.
/// This class extends the <see cref="Client"/> base class to handle token-based authentication.
/// </summary>
public class ClientCredentialsClient : Client {
    readonly TokenService _tokenService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientCredentialsClient"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client used for making requests.</param>
    /// <param name="tokenService">The service responsible for retrieving authentication tokens.</param>
    public ClientCredentialsClient(HttpClient httpClient, TokenService tokenService) : base(httpClient) {
        _tokenService = tokenService;
    }

    /// <summary>
    /// Sends a GET request to the specified endpoint and includes the authorization token in the header.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    /// <param name="endpoint">The API endpoint to send the request to.</param>
    /// <returns>A task representing the asynchronous operation, with the response data of type <typeparamref name="T"/>.</returns>
    protected override async Task<T?> GetAsync<T>(string endpoint) where T : default {
        SetAuthorizationHeader(await _tokenService.GetTokenAsync());
        return await base.GetAsync<T>(endpoint);
    }

    /// <summary>
    /// Sends a POST request to the specified endpoint with the provided data and includes the authorization token in the header.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    /// <param name="endpoint">The API endpoint to send the request to.</param>
    /// <param name="data">The data to be sent in the request body.</param>
    /// <returns>A task representing the asynchronous operation, with the response data of type <typeparamref name="T"/>.</returns>
    protected override async Task<T?> PostAsync<T>(string endpoint, object data) where T : default {
        SetAuthorizationHeader(await _tokenService.GetTokenAsync());
        return await base.PostAsync<T>(endpoint, data);
    }

    /// <summary>
    /// Sends a PUT request to the specified endpoint with the provided data and includes the authorization token in the header.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    /// <param name="endpoint">The API endpoint to send the request to.</param>
    /// <param name="data">The data to be sent in the request body.</param>
    /// <returns>A task representing the asynchronous operation, with the response data of type <typeparamref name="T"/>.</returns>
    protected override async Task<T?> PutAsync<T>(string endpoint, object data) where T : default {
        SetAuthorizationHeader(await _tokenService.GetTokenAsync());
        return await base.PutAsync<T>(endpoint, data);
    }

    /// <summary>
    /// Sends a DELETE request to the specified endpoint and includes the authorization token in the header.
    /// </summary>
    /// <param name="endpoint">The API endpoint to send the request to.</param>
    protected override async Task DeleteAsync(string endpoint) {
        SetAuthorizationHeader(await _tokenService.GetTokenAsync());
        await base.DeleteAsync(endpoint);
    }
}
