using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace AndroThink.Identity.PermissionsAuther
{
    /// <summary>
    /// Extension methods for setting up permissions auther services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class PermissionsAutherSetup
    {
        /// <summary>
        /// register the permissions auther services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">service collection</param>
        public static void UsePermissionsAuther(this IServiceCollection services)
        {
            //Register the Permission policy handlers
            services.AddSingleton<IAuthorizationPolicyProvider,
                PolicyProviders.PermissionsAuthorizationPolicyProvider>();

            services.AddSingleton<IAuthorizationHandler, Handlers.PermissionHandler>();
        }

        /// <summary>
        /// register the permissions auther services to the specified <see cref="IServiceCollection" /> with options.
        /// </summary>
        /// <param name="services">service collection</param>
        /// <param name="options">authorization options</param>
        public static void UsePermissionsAuther(this IServiceCollection services, IOptions<AuthorizationOptions> options)
        {
            //Register the Permission policy handlers
            services.AddSingleton<IAuthorizationPolicyProvider,
                PolicyProviders.PermissionsAuthorizationPolicyProvider>(o => new PolicyProviders.PermissionsAuthorizationPolicyProvider(options));

            services.AddSingleton<IAuthorizationHandler, Handlers.PermissionHandler>();
        }
    }
}
