namespace DivinitySoftworks.Core.Web.Http.Identity;

/// <summary>
/// Configuration settings for connecting to an Identity Server.
/// </summary>
public sealed record IdentityServerSettings {
    /// <summary>
    /// Gets or sets the authority URL of the Identity Server.
    /// </summary>
    public string Authority { get; init; } = default!;

    /// <summary>
    /// Gets or sets the client ID used for authentication with the Identity Server.
    /// </summary>
    public string ClientId { get; init; } = default!;

    /// <summary>
    /// Gets or sets the client secret used for authentication with the Identity Server.
    /// </summary>
    public string ClientSecret { get; init; } = default!;

    /// <summary>
    /// Gets or sets the scope of access requested from the Identity Server.
    /// </summary>
    public string Scope { get; init; } = default!;
}
