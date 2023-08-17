namespace AndroThink.Identity.PermissionsAuther.Interfaces
{
    /// <summary>
    /// Interface for section role used in permission entity in database
    /// </summary>
    public interface ISectionRole
    {
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanSoftDelete { get; set; }
        public short SectionId { get; set; }
    }
}
