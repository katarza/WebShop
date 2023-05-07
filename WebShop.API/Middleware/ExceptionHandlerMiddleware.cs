using System.Text.Json;
using WebShop.Application.Exceptions;
using ApplicationException = WebShop.Application.Exceptions.ApplicationException;
using ValidationException = WebShop.Application.Exceptions.ValidationException;

namespace WebShop.API.Middleware
{
    public class ExceptionHandlerMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            var statusCode = GetStatusCode(exception);

            var response = new
            {
                title = GetTitle(exception),
                status = statusCode,
                detail = exception.Message,
                errors = GetErrors(exception)
            };

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static string GetTitle(Exception exception) =>
            exception switch
            {
                ApplicationException applicationException => applicationException.Title,
                _ => "Server Error"
            };

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };

        private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]> errors = null;

            if (exception is ValidationException validationException)
            {
                errors = validationException.ErrorsDictionary;
            }

            return errors;
        }
    }
}
