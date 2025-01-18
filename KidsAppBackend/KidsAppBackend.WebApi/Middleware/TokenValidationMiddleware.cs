namespace KidsAppBackend.WebApi.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var pathsWithoutToken = new[] { "/api/Auth/registerChild", "/api/Auth/login", "/api/Auth/parentLogin" };

            var requestPath = context.Request.Path.Value?.ToLower() ?? "";
            if (pathsWithoutToken.Any(p => requestPath.Contains(p.ToLower())))
            {
                await _next(context);
                return;
            }

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Token is required");
                return;
            }

            await _next(context);
        }

    }
}
