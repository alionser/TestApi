namespace TestAPI.Web.Middlewares;

public static class ExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseException(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}