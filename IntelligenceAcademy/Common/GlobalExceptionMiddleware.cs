using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using static IntelligenceAcademy.Common.CustomError;

namespace IntelligenceAcademy.Common
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Default to 500
            var code = HttpStatusCode.InternalServerError;
            var message = exception.Message;

            // Handle specific exceptions
            switch (exception)
            {
                case KeyNotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case AppCustomException: // custom error
                    code = HttpStatusCode.OK; // Return 200 for this specific custom error
                    break;
                default:
                    break;
            }

            var result = JsonSerializer.Serialize(new { Success = false, Error = message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }

}
