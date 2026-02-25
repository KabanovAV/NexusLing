using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using NexusLing.Application;
using NexusLing.Infrastructure;
using NexusLing.WebApi.OptionsSetup;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace NexusLing.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSwagger();
            services.AddAuthentication();
            services.AddApplication();
            services.AddInfrastructure(configuration);
            return services;
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("nexusling", new OpenApiInfo
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

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();

                //options.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"
                //            }
                //        },
                //        new string[]{}
                //    }
                //});

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();
            return services;
        }
    }

    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Проверяем наличие атрибута Authorize (на методе или контроллере)
            var hasAuthorize = context.MethodInfo.DeclaringType != null &&
                (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
                || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any());

            // Проверяем наличие AllowAnonymous
            var hasAllowAnonymous = context.MethodInfo.DeclaringType != null &&
                (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any()
                || context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any());

            // Добавляем замок ТОЛЬКО для методов с Authorize И без AllowAnonymous
            if (hasAuthorize && !hasAllowAnonymous)
            {
                // Добавляем коды ответов 401 и 403 в документацию
                operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });

                // Создаем схему безопасности
                var securityScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                // Добавляем требование безопасности ТОЛЬКО для этого метода
                operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    [securityScheme] = new List<string>()
                }
            };
            }
            // Если метод публичный - ничего не добавляем, замка не будет
        }
    }
}
