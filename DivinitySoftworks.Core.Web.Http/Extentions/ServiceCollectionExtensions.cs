using DivinitySoftworks.Core.Web.Http.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DivinitySoftworks.Core.Web.Http.Extentions {
    /// <summary>
    /// Extension methods for setting up token service in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionExtensions {
        /// <summary>
        /// Adds the <see cref="TokenService"/> to the service collection with the specified configuration.
        /// </summary>
        /// <param name="services">The service collection to add the token service to.</param>
        /// <param name="configuration">The configuration used to configure the token service settings.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddTokenService(this IServiceCollection services, IConfiguration configuration) {
            // Configure the IdentityServerSettings using the configuration section
            services.Configure<IdentityServerSettings>(configuration.GetSection("IdentityServer"));

            // Register the TokenService and HttpClient required by the service
            services.AddHttpClient<TokenService>();

            return services;
        }
    }
}
