namespace DivinitySoftworks.Core.Web.Security;
/// <summary>
/// Options for authorization settings.
/// </summary>
public sealed class AuthorizationOptions {
    /// <summary>
    /// Default name of the authorization in the config file.
    /// </summary>
    public const string Authorization = "Authorization";

    /// <summary>
    /// Gets or sets the valid audience for authorization.
    /// </summary>
    public string ValidAudience { get; set; } = default!;

    /// <summary>
    /// Gets or sets the URL of the OpenID Connect metadata.
    /// </summary>
    public string OidcMetadataUrl { get; set; } = default!;
}