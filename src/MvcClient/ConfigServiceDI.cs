using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcClient.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MvcClient
{
    public static class ConfigServiceDI
    {
        public static IServiceCollection MVCServices(this IServiceCollection services, IConfiguration configuration)
        {

            // JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Add("sub", ClaimTypes.NameIdentifier);

            //  services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
            //     configuration.GetConnectionString("DefaultConnection"),
            //             builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
            ));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect("oidc", options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                options.Authority = "https://localhost:5001";

                options.ClientId = "mvc";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                options.RequireHttpsMetadata = false;
                options.UsePkce = true;

                options.SaveTokens = true;

                options.Scope.Add("api1");
                options.Scope.Add("offline_access");
                
                options.ClaimActions.MapJsonKey("website", "website");
            });

            return services;
        }
    }
}
