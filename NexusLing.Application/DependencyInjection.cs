using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NexusLing.Application.Common.Interfaces;
using NexusLing.Application.Interfaces;
using NexusLing.Application.Services;
using System.Reflection;

namespace NexusLing.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services.AddUserService().AddAuthService().AddValidationService();

        public static IServiceCollection AddUserService(this IServiceCollection services)
            => services.AddScoped<IUserService, UserService>();

        public static IServiceCollection AddAuthService(this IServiceCollection services)
            => services.AddScoped<IAuthService, AuthService>();

        public static IServiceCollection AddValidationService(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidationService, ValidationService>();
            return services;
        }
    }
}
