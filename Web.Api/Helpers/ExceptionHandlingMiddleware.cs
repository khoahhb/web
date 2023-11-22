using Newtonsoft.Json;
using System.Net;

namespace Web.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
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
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = _env.IsDevelopment()
                ? JsonConvert.SerializeObject(new { error = exception.ToString() })
                : JsonConvert.SerializeObject(new { error = "An unexpected error has occurred." });

            //Console.WriteLine(exception.ToString());
            //var result = JsonConvert.SerializeObject(new { error = "An unexpected error has occurred." });

            return context.Response.WriteAsync(result);
        }
    }

}
