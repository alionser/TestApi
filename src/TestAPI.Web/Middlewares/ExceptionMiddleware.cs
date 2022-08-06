using System.Net;
using System.Text.Json;
using TestAPI.Web.ResponseModels;

namespace TestAPI.Web.Middlewares;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate requestDelegate)
    {
        _next = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception exception)
        {
            var result = HandleException(context, exception);
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }

    private static ResponseModel HandleException(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            BadHttpRequestException badRequestException => badRequestException.StatusCode,
            _ => (int)HttpStatusCode.InternalServerError,
        };

        context.Response.StatusCode = statusCode;
        var result = new ResponseModel
        {
            Error = exception.Message
        };

        return result;
    }
}