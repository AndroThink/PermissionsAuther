namespace AndroThink.Identity.PermissionsAuther.Options
{
    /// <summary>
    /// Model represent JWT options
    /// </summary>
    public class JWTOptions
    {
        public string ValidIssuer { get; set; } = "";
        public string ValidAudience { get; set; } = "";
        public string IssuerSigningKey { get; set; } = "";
        public string TokenDecryptionKey { get; set; } = "";
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
    }
}
