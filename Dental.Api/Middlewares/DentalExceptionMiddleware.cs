using Dental.Domain.Models.Response;
using Dental.Service.Exceptions;
using System.Runtime.InteropServices;

namespace Dental.Api.Middlewares;

public class DentalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<DentalExceptionMiddleware> _logger;

    public DentalExceptionMiddleware(RequestDelegate next, ILogger<DentalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (DentalException ex)
        {
            await HandleException(context, ex.Code, ex.Message, ex.Global);
        }
        catch (Exception ex)
        {
            //Log
            _logger.LogError(ex.ToString());

            await HandleException(context, 500, "", true);
        }
    }

    public async Task HandleException(HttpContext context, int code, string message, bool? Global)
    {
        context.Response.StatusCode = code;
        await context.Response.WriteAsJsonAsync(
            new ResponseModel<string>
            {
                Status = false,
                Error = message,
                Data = null,
                GlobalError = Global
            }
        );
    }
}
