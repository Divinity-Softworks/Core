using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace DivinitySoftworks.Core.Web.Http;

/// <summary>
/// Abstract base class for API clients that provides methods for making HTTP requests.
/// </summary>
public abstract class Client {
    protected readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="Client"/> class with the specified <see cref="HttpClient"/>.
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/> instance to be used for making HTTP requests.</param>
    protected Client(HttpClient httpClient) {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Sets the Authorization header with the specified token.
    /// </summary>
    /// <param name="token">The token to be used for authorization.</param>
    /// <param name="token_type">The type of token issued.</param>
    public Client SetAuthorizationHeader(string token, string token_type = "Bearer") {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token_type, token);
        return this;
    }

    /// <summary>
    /// Sends a GET request to the specified endpoint and returns the response deserialized to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to which the response content should be deserialized.</typeparam>
    /// <param name="endpoint">The endpoint to which the GET request is sent.</param>
    /// <returns>A task representing the asynchronous operation, with a result of the deserialized response content.</returns>
    /// <exception cref="HttpRequestException">Thrown if the HTTP response indicates a failure.</exception>
    protected virtual async Task<T?> GetAsync<T>(string endpoint) {
        HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
        return await HandleResponse<T>(response);
    }

    /// <summary>
    /// Sends a POST request to the specified endpoint with the specified data and returns the response deserialized to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to which the response content should be deserialized.</typeparam>
    /// <param name="endpoint">The endpoint to which the POST request is sent.</param>
    /// <param name="data">The data to be sent in the POST request body.</param>
    /// <returns>A task representing the asynchronous operation, with a result of the deserialized response content.</returns>
    /// <exception cref="HttpRequestException">Thrown if the HTTP response indicates a failure.</exception>
    protected virtual async Task<T?> PostAsync<T>(string endpoint, object data) {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(endpoint, data);
        return await HandleResponse<T>(response);
    }

    /// <summary>
    /// Sends a PUT request to the specified endpoint with the specified data and returns the response deserialized to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to which the response content should be deserialized.</typeparam>
    /// <param name="endpoint">The endpoint to which the PUT request is sent.</param>
    /// <param name="data">The data to be sent in the PUT request body.</param>
    /// <returns>A task representing the asynchronous operation, with a result of the deserialized response content.</returns>
    /// <exception cref="HttpRequestException">Thrown if the HTTP response indicates a failure.</exception>
    protected virtual async Task<T?> PutAsync<T>(string endpoint, object data) {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync(endpoint, data);
        return await HandleResponse<T>(response);
    }

    /// <summary>
    /// Sends a DELETE request to the specified endpoint.
    /// </summary>
    /// <param name="endpoint">The endpoint to which the DELETE request is sent.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="HttpRequestException">Thrown if the HTTP response indicates a failure.</exception>
    protected virtual async Task DeleteAsync(string endpoint) {
        HttpResponseMessage response = await _httpClient.DeleteAsync(endpoint);
        await HandleResponse(response);
    }

    /// <summary>
    /// Handles the HTTP response by checking for success status and deserializing the response content to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to which the response content should be deserialized.</typeparam>
    /// <param name="response">The HTTP response message.</param>
    /// <returns>A task representing the asynchronous operation, with a result of the deserialized response content.</returns>
    /// <exception cref="HttpRequestException">Thrown if the HTTP response indicates a failure.</exception>
    protected async Task<T?> HandleResponse<T>(HttpResponseMessage response) {
        if (!response.IsSuccessStatusCode) {
            string errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error: {response.StatusCode}, Content: {errorContent}");
        }

        if(response.StatusCode == HttpStatusCode.NoContent)
            return default;

        string content = await response.Content.ReadAsStringAsync();

        return string.IsNullOrWhiteSpace(content) ? default : JsonSerializer.Deserialize<T>(content);
    }

    /// <summary>
    /// Handles the HTTP response by checking for success status.
    /// </summary>
    /// <param name="response">The HTTP response message.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="HttpRequestException">Thrown if the HTTP response indicates a failure.</exception>
    protected async Task HandleResponse(HttpResponseMessage response) {
        if (!response.IsSuccessStatusCode) {
            string errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error: {response.StatusCode}, Content: {errorContent}");
        }
    }
}
