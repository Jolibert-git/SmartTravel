
using System.Net;
using System.Text.Json;
using Travel.Application.Responses;
using Travel.Domain.Exceptions;


namespace Travel.API.Midlewera
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción no controlada: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            object response;

            switch (exception)
            {
                case ValidationException ve:
                    context.Response.StatusCode = (int)ve.StatusCode;
                    response = ApiResponse<object>.Fail(
                        ve.Message,
                        (int)ve.StatusCode,
                        ve.Errors);
                    break;

                case AppException ae:
                    context.Response.StatusCode = (int)ae.StatusCode;
                    response = ApiResponse<object>.Fail(ae.Message, (int)ae.StatusCode);
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = ApiResponse<object>.Fail(
                        "Ocurrió un error interno en el servidor.",
                        (int)HttpStatusCode.InternalServerError);
                    break;
            }

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
    }
}
