using TestAPI.Web.Middlewares;

namespace TestAPI.Web.Extentsions;

public static class ExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseException(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}