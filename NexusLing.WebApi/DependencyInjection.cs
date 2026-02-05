using Microsoft.OpenApi.Models;
using NexusLing.Infrastructure;
using System.Reflection;

namespace NexusLing.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSwagger();
            services.AddInfrastructure(configuration);
            return services;
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("NexusLing", new OpenApiInfo
                {
                    Title = "NexusLing API",
                    Version = "v0",
                    Description = "Description of NexusLing API",
                    TermsOfService = new Uri("https://nexusling/privacy-policy"),
                    Contact = new OpenApiContact
                    {
                        Name = "NexusLing",
                        Email = "sarnaut@mail.com",
                        Url = new Uri("https://nexusling/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "NexusLing License",
                        Url = new Uri("https://nexusling/about-us")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            return services;
        }
    }
}
