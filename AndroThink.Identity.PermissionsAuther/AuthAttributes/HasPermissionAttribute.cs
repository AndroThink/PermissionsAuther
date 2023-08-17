using System;
using Microsoft.AspNetCore.Authorization;

namespace AndroThink.Identity.PermissionsAuther.AuthAttributes
{
    /// <summary>
    /// Has permission attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Has permission attribute
        /// </summary>
        /// <param name="sectionId">section id</param>
        /// <param name="permission">permission required to access this section</param>
        public HasPermissionAttribute(short sectionId, Enums.Permissions permission) : base(PermissionUtils.CreatePermissionRole(sectionId, permission)) { }
    }
}
