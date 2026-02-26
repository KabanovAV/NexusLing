using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NexusLing.Application.Interfaces;
using NexusLing.Domain.Interfaces;
using NexusLing.Infrastructure.Authentications;
using NexusLing.Infrastructure.Database;
using NexusLing.Infrastructure.Repositories;
using System.Text;

namespace NexusLing.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
            => services.AddDatabase(configuration)
                .AddRepository()
                .AddAuthenticationInternal(configuration)
                .AddAuthorizationInternal();

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IApplicationDbContext>(options => options.GetRequiredService<ApplicationDbContext>());
            return services;
        }

        private static IServiceCollection AddRepository(this IServiceCollection services)
            => services.AddScoped<IRepository, Repository>();

        private static IServiceCollection AddAuthenticationInternal(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            return services;
        }

        private static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
            => services.AddAuthorization();
    }
}
