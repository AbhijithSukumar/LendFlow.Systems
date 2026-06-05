namespace LendFlow.Systems.API.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var response = new
                {
                    context.Response.StatusCode,
                    Message = "Internal Server Error. Please try again later.",
                    Details = ex.Message // Optional: Only show this in Development mode
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
