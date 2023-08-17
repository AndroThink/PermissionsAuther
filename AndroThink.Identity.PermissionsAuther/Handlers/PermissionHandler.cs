using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AndroThink.Identity.PermissionsAuther.Handlers
{
    /// <summary>
    /// Permission authorization handler
    /// </summary>
    public sealed class PermissionHandler : IAuthorizationHandler
    {
        /// <summary>
        /// Handle the permission authorization
        /// </summary>
        /// <param name="context">authorization handler context</param>
        /// <returns>Success if permission found continue otherwise</returns>
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();
            var permissionsClaim = context.User.Claims.SingleOrDefault(c => c.Type == CustomClaimTypes.PermissionClaimType);

            if (permissionsClaim == null)
                return Task.CompletedTask;

            foreach (var requirement in pendingRequirements)
            {
                if (requirement is Requirements.PermissionRequirement)
                {
                    if (permissionsClaim.Value.ThisPermissionIsAllowed(((Requirements.PermissionRequirement)requirement).PermissionName))
                        context.Succeed(requirement);
                }
                else if (requirement is Requirements.PermissionArrayRequirement)
                {
                    foreach (var permisssion in ((Requirements.PermissionArrayRequirement)requirement).PermissionNames.UnpackPermissionsFromString())
                    {
                        if (permissionsClaim.Value.ThisPermissionIsAllowed(permisssion))
                        {
                            context.Succeed(requirement);
                            return Task.CompletedTask;
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
