namespace AndroThink.Identity.PermissionsAuther.Enums
{
    /// <summary>
    /// Enum for permissions actions
    /// </summary>
    public enum Permissions : short
    {
        CanView = 0x10,
        CanAdd = 0x11,
        CanEdit = 0x12,
        CanDelete = 0x13,
        CanSoftDelete = 0x14
    }
}
