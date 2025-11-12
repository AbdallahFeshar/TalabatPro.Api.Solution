using System.Net;
using System.Text.Json;

namespace TalabatPro.Api.MiddleWare
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate next ,ILogger<ExceptionMiddleWare>logger,IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = _env.IsDevelopment()
                    ? new Errors.ApiExceptionMiddleWare((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString())
                    : new Errors.ApiExceptionMiddleWare((int)HttpStatusCode.InternalServerError, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
            }
        }
    }
}
