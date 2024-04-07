using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment hostEnvironment;

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger, IHostEnvironment hostEnvironment)
        {
            this.next = next;
            this.logger = logger;
            this.hostEnvironment = hostEnvironment;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex , ex.Message);
                 
                 context.Response.ContentType= "application/json";

                var exceotionErrorRespons = hostEnvironment.IsDevelopment() ?
                    new APIsExceptionResponse(500, ex.Message, ex.StackTrace.ToString())
                    :
                    new APIsExceptionResponse(500);

                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(exceotionErrorRespons , options);

                await context.Response.WriteAsync(json);
            }


            }
    }
}
