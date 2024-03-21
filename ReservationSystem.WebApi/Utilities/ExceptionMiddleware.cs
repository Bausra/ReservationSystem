using ReservationSystem.WebApi.Models;
using System.Text.Json;

namespace ReservationSystem.WebApi.Helpers
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public readonly ILogger<ExceptionMiddleware> _logger;

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
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            ErrorInformation exModel = new ErrorInformation();

            response.StatusCode = 500;
            exModel.StatusCode = 500;
            exModel.ErrorMessage = exception.Message;

            var result = JsonSerializer.Serialize(exModel);

            _logger.LogError(exception.ToString());
            
            return context.Response.WriteAsync(result);
        }
    }
}
