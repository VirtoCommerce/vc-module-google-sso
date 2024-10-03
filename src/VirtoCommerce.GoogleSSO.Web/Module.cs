using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.GoogleSSO.Core.Models;
using VirtoCommerce.GoogleSSO.Data.Services;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Security.ExternalSignIn;
using VirtoCommerce.Platform.Security.ExternalSignIn;

namespace VirtoCommerce.GoogleSSO.Web;

public class Module : IModule, IHasConfiguration
{
    public ManifestModuleInfo ModuleInfo { get; set; }
    public IConfiguration Configuration { get; set; }

    public void Initialize(IServiceCollection serviceCollection)
    {
        var googleSsoSection = Configuration.GetSection("GoogleSSO");

        if (googleSsoSection.GetChildren().Any())
        {
            var options = new GoogleSsoOptions();
            googleSsoSection.Bind(options);
            serviceCollection.Configure<GoogleSsoOptions>(googleSsoSection);

            if (options.Enabled)
            {
                var authBuilder = new AuthenticationBuilder(serviceCollection);

                //https://docs.microsoft.com/en-us/azure/active-directory/develop/microsoft-identity-web
                authBuilder.AddOpenIdConnect(options.AuthenticationType, options.AuthenticationCaption,
                    openIdConnectOptions =>
                    {
                        openIdConnectOptions.ClientId = options.ApplicationId;
                        openIdConnectOptions.ClientSecret = options.Secret;

                        openIdConnectOptions.Authority = "https://accounts.google.com";
                        openIdConnectOptions.UseTokenLifetime = true;
                        openIdConnectOptions.RequireHttpsMetadata = false;
                        openIdConnectOptions.CallbackPath = new PathString("/signin-google");
                        openIdConnectOptions.SignInScheme = IdentityConstants.ExternalScheme;

                        openIdConnectOptions.Scope.Add("openid");
                        openIdConnectOptions.Scope.Add("profile");
                        openIdConnectOptions.Scope.Add("email");

                        var serviceDescriptor = serviceCollection.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(JwtSecurityTokenHandler));
                        if (serviceDescriptor?.ImplementationInstance is JwtSecurityTokenHandler defaultTokenHandler)
                        {
                            openIdConnectOptions.UseSecurityTokenValidator = true;
#pragma warning disable CS0618 // Type or member is obsolete
                            openIdConnectOptions.SecurityTokenValidator = defaultTokenHandler;
#pragma warning restore CS0618 // Type or member is obsolete

                            openIdConnectOptions.Events.OnRedirectToIdentityProvider = context =>
                            {
                                var oidcUrl = context.Properties.GetOidcUrl();
                                if (!string.IsNullOrEmpty(oidcUrl))
                                {
                                    context.ProtocolMessage.RedirectUri = oidcUrl;
                                }
                                return Task.CompletedTask;
                            };
                        }
                    });

                // register default external provider implementation
                serviceCollection.AddSingleton<GoogleSSOExternalSignInProvider>();
                serviceCollection.AddSingleton(provider => new ExternalSignInProviderConfiguration
                {
                    AuthenticationType = "GoogleSSO",
                    Provider = provider.GetService<GoogleSSOExternalSignInProvider>(),
                    LogoUrl = "Modules/$(VirtoCommerce.GoogleSSO)/Content/provider-logo.webp"
                });
            }
        }
    }

    public void PostInitialize(IApplicationBuilder appBuilder)
    {
        // Nothing to do here
    }

    public void Uninstall()
    {
        // Nothing to do here
    }
}
