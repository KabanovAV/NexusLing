using NexusLing.Application.Common.Exceptions;
using NexusLing.Domain.Exceptions;
using System.Text.Json;

namespace NexusLing.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
            }
            catch (DomainException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
            }
            catch (ValidatorException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    status = 400,
                    title = "Ошибка валидации",
                    message = ex.Message,
                    errors = ex.Errors
                };

                await context.Response.WriteAsJsonAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
