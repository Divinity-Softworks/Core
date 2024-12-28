using DivinitySoftworks.Core.Web.Security;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for the <see cref="IServiceCollection"/> interface.
/// </summary>
public static class IServiceCollectionExtensions {

    /// <summary>
    /// Adds OpenID Connect authorization services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to which the services will be added.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddOpenIdConnect(this IServiceCollection services) {
        services.AddSingleton<IAuthorizeService, AuthorizeService>();

        return services;
    }
}
