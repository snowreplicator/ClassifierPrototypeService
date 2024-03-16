using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Prototype.ClassifierPrototypeService.Application.Common;
using Prototype.ClassifierPrototypeService.Bll.Common;
using Prototype.ClassifierPrototypeService.Infrastructure.common;

namespace Prototype.ClassifierPrototypeService.Middleware;

public class WrapServiceExceptionsMiddleware
{
    private readonly RequestDelegate _next;

    public WrapServiceExceptionsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (InfrastructureLayerException ile)
        {
            await HandleException(httpContext, ile, ile.Code, ile.Message);
        }
        catch (ApplicationLayerException ale)
        {
            await HandleException(httpContext, ale, ale.Code, ale.Message);
        }
        catch (BllLayerException ble)
        {
            await HandleException(httpContext, ble, ble.Code, ble.Message);
        }        
        catch (Exception e)
        {
            await HandleException(httpContext, e, Error.S100ErrorHandlingRequest, e.Message);
        }
    }
    
    private static Task HandleException(HttpContext context, Exception exception, string code, string message)
    {
        var result = JsonConvert.SerializeObject(
            new
            {
                IsError = true,
                Code = code, 
                Message = message,
                ExceptionType = exception.GetType(),
                Source = exception.Source,
                Exception = exception,
            });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(result);
    }

}
