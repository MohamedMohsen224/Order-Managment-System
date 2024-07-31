using Order_Managment_System.ErrorResponse;
using System.Net;
using System.Text.Json;

namespace Order_Managment_System.MiddleWare
{
    public class MiddlewareException
    {
        private readonly RequestDelegate next;
        private readonly ILogger<MiddlewareException> logger;
        private readonly IHostEnvironment env;

        public MiddlewareException (RequestDelegate next,ILogger<MiddlewareException> logger,IHostEnvironment env )
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try { 

                await next.Invoke(context);
            
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = env.IsDevelopment()
                    ? new ApiExceptionError((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiExceptionError((int)HttpStatusCode.InternalServerError);
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
