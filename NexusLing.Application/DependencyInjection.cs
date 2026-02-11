using Microsoft.Extensions.DependencyInjection;

namespace NexusLing.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services;
    }
}
