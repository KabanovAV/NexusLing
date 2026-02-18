using NexusLing.Application.Common.Exceptions;
using NexusLing.Domain.Exceptions;
using Serilog;

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
                Log.Warning(ex, "Ресурс не найден: {Path}", context.Request.Path);
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new
                {
                    status = 404,
                    title = "Ресурс не найден",
                    message = ex.Message,
                    path = context.Request.Path
                });
            }
            catch (DomainException ex)
            {
                Log.Warning(ex, "Ошибка домена: {Path}", context.Request.Path);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new
                {
                    status = 400,
                    title = "Ошибка бизнес-логики",
                    message = ex.Message,
                    path = context.Request.Path
                });
            }
            catch (ValidatorException ex)
            {
                Log.Warning(ex, "Ошибка валидации: {Path} метод {Method}", context.Request.Path, context.Request.Method);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new
                {
                    status = 400,
                    title = "Ошибка валидации",
                    message = ex.Message,
                    errors = ex.Errors,
                    path = context.Request.Path
                });
            }
        }
    }
}
