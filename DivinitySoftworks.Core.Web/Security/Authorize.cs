namespace DivinitySoftworks.Core.Web.Security;

/// <summary>
/// Specifies the authorization requirements for a function.
/// </summary>
public enum Authorize {
    /// <summary>
    /// The authorization requirement is unknown.
    /// </summary>
    Unknown = -1,

    /// <summary>
    /// Authorization is required to access the function.
    /// </summary>
    Required,

    /// <summary>
    /// The function allows anonymous access without authorization.
    /// </summary>
    AllowAnonymous
}
