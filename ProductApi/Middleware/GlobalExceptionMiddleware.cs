namespace ProductApi.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
                _logger.LogError(ex, "Beklenmeyen bir hata oluştu: {Message}", ex.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    status = 500,
                    message = "Sunucuda beklenmeyen bir hata oluştu.",

                    detail = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                        == "Development" ? ex.Message : null
                };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}