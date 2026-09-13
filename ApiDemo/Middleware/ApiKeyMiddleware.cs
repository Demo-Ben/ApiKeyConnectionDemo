namespace ApiDemo.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HeaderName = "X-API-KEY";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration config)
        {
            //if (!context.Request.Headers.TryGetValue(HeaderName, out var extractedKey))
            //{
            //    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //    await context.Response.WriteAsJsonAsync(new { error = "API Key missing" });
            //    return;
            //}

            //var apiKey = config.GetValue<string>("ApiKey");

            //if (!apiKey.Equals(extractedKey))
            //{
            //    context.Response.StatusCode = StatusCodes.Status403Forbidden;
            //    await context.Response.WriteAsJsonAsync(new { error = "Invalid API Key" });
            //    return;
            //}

            //await _next(context);

            if (!context.Request.Headers.TryGetValue(HeaderName, out var extractedKey))
            {
                await Results.Json(
                    new { error = "API Key missing" },
                    statusCode: StatusCodes.Status401Unauthorized
                ).ExecuteAsync(context);

                return;
            }

            var apiKey = config.GetValue<string>("ApiKey");

            if (!apiKey.Equals(extractedKey))
            {
                await Results.Json(
                    new { error = "Invalid API Key" },
                    statusCode: StatusCodes.Status403Forbidden
                ).ExecuteAsync(context);

                return;
            }

            await _next(context);
        }
    }


}
