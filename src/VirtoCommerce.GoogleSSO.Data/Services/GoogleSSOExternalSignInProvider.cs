using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using VirtoCommerce.GoogleSSO.Core.Models;
using VirtoCommerce.Platform.Security.ExternalSignIn;

namespace VirtoCommerce.GoogleSSO.Data.Services;

public class GoogleSSOExternalSignInProvider(IOptions<GoogleSsoOptions> googleSsoOptions) : IExternalSignInProvider
{
    private readonly GoogleSsoOptions _googleSsoOptions = googleSsoOptions.Value;

    public bool AllowCreateNewUser => _googleSsoOptions.AllowCreateNewUser;

    public int Priority => _googleSsoOptions.Priority;

    public bool HasLoginForm => _googleSsoOptions.HasLoginForm;

    public string GetUserName(ExternalLoginInfo externalLoginInfo)
    {
        var userName = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrWhiteSpace(userName) && _googleSsoOptions.UsePreferredUsername)
        {
            userName = externalLoginInfo.Principal.FindFirstValue("name");
        }

        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new InvalidOperationException("Received external login info does not have an UPN claim or DefaultUserName.");
        }

        return userName;
    }

    public string GetUserType()
    {
        return _googleSsoOptions.DefaultUserType;
    }

    public string[] GetUserRoles()
    {
        return _googleSsoOptions.DefaultUserRoles;
    }
}
