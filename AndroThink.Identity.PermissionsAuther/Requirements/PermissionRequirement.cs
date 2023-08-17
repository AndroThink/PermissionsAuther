using Microsoft.AspNetCore.Authorization;
using System;

namespace AndroThink.Identity.PermissionsAuther.Requirements
{
    /// <summary>
    /// Permission requirement for single permission
    /// </summary>
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string permissionName) => 
            PermissionName = permissionName ?? throw new ArgumentNullException(nameof(permissionName));

        public string PermissionName { get; }
    }
}
