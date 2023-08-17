using System.Linq;
using System.Collections.Generic;

namespace AndroThink.Identity.PermissionsAuther
{
    /// <summary>
    /// Permission utils methods
    /// </summary>
    public static class PermissionUtils
    {
        /// <summary>
        /// Generate permission role schema
        /// </summary>
        /// <param name="section">short represent the id of the section </param>
        /// <param name="permission">permission that availavle for this section</param>
        /// <returns>string represent the role (section with its permission)</returns>
        internal static string CreatePermissionRole(short section, Enums.Permissions permission) => $"{section}_{((short)permission)}";

        /// <summary>
        /// Geneteate permission(s) claim from application's sections roles
        /// </summary>
        /// <param name="sectionRoles">list of the sections with their permissions applied on them</param>
        /// <returns>claim contains all available sections and their permissions</returns>
        public static System.Security.Claims.Claim CrearePermissionClaim(IEnumerable<Interfaces.ISectionRole> sectionRoles)
        {
            var permissions = CreatePermissionRoles(sectionRoles);
            return new System.Security.Claims.Claim(CustomClaimTypes.PermissionClaimType, permissions.PackPermissionsIntoString());
        }

        /// <summary>
        /// Generate list permission role schema from application's sections roles
        /// </summary>
        /// <param name="sectionRoles">list of the sections with their permissions applied on them</param>
        /// <returns>list of section permission role</returns>
        internal static List<string> CreatePermissionRoles(IEnumerable<Interfaces.ISectionRole> sectionRoles)
        {
            List<string> permissions = new List<string>();

            foreach (var sectionRole in sectionRoles)
            {
                permissions.AddRange(sectionRole.CreatePermissionRoles());
            }

            return permissions;
        }

        /// <summary>
        /// Generate list permission role schema from application's sections role
        /// </summary>
        /// <param name="sectionRole">sections with their permissions applied on them</param>
        /// <returns>list of section permission roles</returns>
        internal static List<string> CreatePermissionRoles(this Interfaces.ISectionRole sectionRole)
        {
            List<string> permissions = new List<string>();

            if (sectionRole.CanView)
                permissions.Add(CreatePermissionRole(sectionRole.SectionId, Enums.Permissions.CanView));
            if (sectionRole.CanAdd)
                permissions.Add(CreatePermissionRole(sectionRole.SectionId, Enums.Permissions.CanAdd));
            if (sectionRole.CanDelete)
                permissions.Add(CreatePermissionRole(sectionRole.SectionId, Enums.Permissions.CanDelete));
            if (sectionRole.CanSoftDelete)
                permissions.Add(CreatePermissionRole(sectionRole.SectionId, Enums.Permissions.CanSoftDelete));
            if (sectionRole.CanEdit)
                permissions.Add(CreatePermissionRole(sectionRole.SectionId, Enums.Permissions.CanEdit));

            return permissions;
        }

        /// <summary>
        /// Pack permissions into string
        /// </summary>
        /// <param name="permissions">list of section permission roles</param>
        /// <returns>one string contains all section roles separated by comma , </returns>
        internal static string PackPermissionsIntoString(this IEnumerable<string> permissions) => string.Join(",", permissions);

        /// <summary>
        /// Unpack permissions from string
        /// </summary>
        /// <param name="packedPermissions">string contains all section roles separated by comma , </param>
        /// <returns>list of section permission roles</returns>
        internal static IEnumerable<string> UnpackPermissionsFromString(this string packedPermissions) => packedPermissions.Split(",");

        /// <summary>
        /// Check if this permission is allowed for this user by comparing it with all permissions allowed to the user
        /// </summary>
        /// <param name="packedPermissions">string contains all section roles separated by comma , </param>
        /// <param name="permissionName">permission to be validated</param>
        /// <returns>true if user owns this permission, false otherwise</returns>
        internal static bool ThisPermissionIsAllowed(this string packedPermissions, string permissionName)
        {
            var usersPermissions = packedPermissions.UnpackPermissionsFromString().ToArray();
            return usersPermissions.Any() && usersPermissions.Contains(permissionName);
        }
    }
}
