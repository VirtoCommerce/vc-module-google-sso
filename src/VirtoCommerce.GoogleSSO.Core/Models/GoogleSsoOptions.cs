namespace VirtoCommerce.GoogleSSO.Core.Models;

public class GoogleSsoOptions
{
    /// <summary>
    /// Determines whether the user authentication via Google SSO is enabled.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Sets AuthenticationType value for Google SSO authentication provider.
    /// </summary>
    public string AuthenticationType { get; set; }

    /// <summary>
    /// Sets human-readable caption for Google SSO authentication provider. It is visible on sign-in page.
    /// </summary>
    public string AuthenticationCaption { get; set; }

    /// <summary>
    /// Application ID of the VirtoCommerce platform application registered in Google Developer Console.
    /// </summary>
    public string ApplicationId { get; set; }

    /// <summary>
    /// Client Secret for Google SSO authentication provider.
    /// </summary>
    public string Secret { get; set; }

    /// <summary>
    /// Default user type for users created by Google SSO accounts.
    /// </summary>
    public string DefaultUserType { get; set; }

    /// <summary>
    /// Default user roles for users created by Google SSO accounts.
    /// </summary>
    public string[] DefaultUserRoles { get; set; }

    /// <summary>
    /// Check preferred_username claim as a fallback scenario in case when UPN claim is not set
    /// </summary>
    public bool UsePreferredUsername { get; set; }

    /// <summary>
    /// Login type priority
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Allow creating new user when a user authenticates via AD for the first time
    /// </summary>
    public bool AllowCreateNewUser { get; set; } = true;

    /// <summary>
    /// Display dedicated login form or not
    /// </summary>
    public bool HasLoginForm { get; set; } = true;
}
