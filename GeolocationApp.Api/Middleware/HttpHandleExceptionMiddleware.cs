using System.Net;
using System.Text.Json;
using GeolocationApp.Domain.Exceptions;

namespace GeolocationApp.Api.Middleware
{
    public class HttpHandleExceptionMiddleware(RequestDelegate next, ILogger<HttpHandleExceptionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Internal server error";
            var errorId = Guid.NewGuid();
            if (exception is BaseException baseException)
            {
                statusCode = baseException.StatusCode;
                message = baseException.Message;
            }

            logger.LogError(exception, $"Error ID: {errorId}");

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            var errorResponse = new ErrorResponse
            {
                ErrorCode = (int)statusCode,
                Message = message,
                ErrorId = errorId.ToString()
            };

            var errorJson = JsonSerializer.Serialize(errorResponse);
            return response.WriteAsync(errorJson);
        }
    }
}