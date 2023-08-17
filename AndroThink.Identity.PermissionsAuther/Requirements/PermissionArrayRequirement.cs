using System;
using Microsoft.AspNetCore.Authorization;

namespace AndroThink.Identity.PermissionsAuther.Requirements
{
    /// <summary>
    /// Permission requirement for multiple permission
    /// </summary>
    public class PermissionArrayRequirement : IAuthorizationRequirement
    {
        public string PermissionNames { get; }

        public PermissionArrayRequirement(string permissionNames) =>
            PermissionNames = permissionNames ?? throw new ArgumentNullException(nameof(permissionNames));
    }
}
