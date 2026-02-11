using Microsoft.Extensions.DependencyInjection;
using NexusLing.Application.Interfaces;
using NexusLing.Application.Services;

namespace NexusLing.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services.AddUserService();

        public static IServiceCollection AddUserService(this IServiceCollection services)
            => services.AddScoped<IUserService, UserService>();
    }
}
