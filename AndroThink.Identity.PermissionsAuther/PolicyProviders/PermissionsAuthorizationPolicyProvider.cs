using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace AndroThink.Identity.PermissionsAuther.PolicyProviders
{
    /// <summary>
    /// Permission policy provider
    /// </summary>
    public sealed class PermissionsAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _options;

        public PermissionsAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// Get policy from permission by name or create new policy with permission name
        /// </summary>
        /// <param name="policyName">policy name</param>
        /// <returns>authorization policy</returns>
        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);

            if (policy == null)
            {
                if (policyName.Contains(","))
                    policy = new AuthorizationPolicyBuilder()
                               .AddRequirements(new Requirements.PermissionArrayRequirement(policyName))
                               .Build();
                else
                    policy = new AuthorizationPolicyBuilder()
                           .AddRequirements(new Requirements.PermissionRequirement(policyName))
                           .Build();
            }

            return policy;
        }
    }
}
