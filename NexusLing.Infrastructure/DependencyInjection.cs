using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexusLing.Application.Interfaces;
using NexusLing.Domain.Interfaces;
using NexusLing.Infrastructure.Authentications;
using NexusLing.Infrastructure.Database;
using NexusLing.Infrastructure.Repositories;

namespace NexusLing.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
            => services.AddDatabase(configuration)
                .AddRepository()
                .AddPasswordHasher()
                .AddJwtProvider();

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IApplicationDbContext>(options => options.GetRequiredService<ApplicationDbContext>());
            return services;
        }

        private static IServiceCollection AddRepository(this IServiceCollection services)
            => services.AddScoped<IRepository, Repository>();

        private static IServiceCollection AddPasswordHasher(this IServiceCollection services)
            => services.AddScoped<IPasswordHasher, PasswordHasher>();

        private static IServiceCollection AddJwtProvider(this IServiceCollection services)
           => services.AddScoped<IJwtProvider, JwtProvider>();
    }
}
