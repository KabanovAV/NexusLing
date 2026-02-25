using NexusLing.Application.Common.Exceptions;
using NexusLing.Domain.Common.Exceptions;
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
                Log.Warning(ex, "Ресурс не найден: {Path} метод {Method}", context.Request.Path, context.Request.Method);
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new
                {
                    status = 404,
                    title = "Ресурс не найден",
                    message = ex.Message,
                    path = context.Request.Path
                });
            }
            catch (NotEqualIdException ex)
            {
                Log.Warning(ex, "Id ресурсов не совпадают: {Path} метод {Method}", context.Request.Path, context.Request.Method);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new
                {
                    status = 400,
                    title = "Ресурсы не совпадают",
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
            catch (DomainException ex)
            {
                Log.Warning(ex, "Ошибка домена: {Path} метод {Method}", context.Request.Path, context.Request.Method);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new
                {
                    status = 400,
                    title = "Ошибка бизнес-логики",
                    message = ex.Message,
                    path = context.Request.Path
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Необработанная ошибка: {Path} метод {Method}", context.Request.Path, context.Request.Method);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new
                {
                    status = 500,
                    title = "Внутренняя ошибка сервера",
                    message = "Произошла внутренняя ошибка. Пожалуйста, попробуйте позже.",
                    path = context.Request.Path
                });
            }
        }
    }
}
