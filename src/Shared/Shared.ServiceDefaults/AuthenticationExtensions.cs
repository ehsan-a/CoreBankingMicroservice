using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Shared.ServiceDefaults
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddDefaultAuthentication(this IHostApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.Configuration;

            var jwtSection = configuration.GetSection("jwt");

            if (!jwtSection.Exists())
            {
                return services;
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSection.GetRequiredValue("Issuer"),
                        ValidAudience = jwtSection.GetRequiredValue("Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSection.GetRequiredValue("Key"))
                        ),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            return services;
        }
    }
}
