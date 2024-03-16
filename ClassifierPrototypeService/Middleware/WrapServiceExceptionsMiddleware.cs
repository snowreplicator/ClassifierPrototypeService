using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Prototype.ClassifierPrototypeService.Application.Common;
using Prototype.ClassifierPrototypeService.Bll.Common;
using Prototype.ClassifierPrototypeService.Infrastructure.common;

namespace Prototype.ClassifierPrototypeService.Middleware;

public class WrapServiceExceptionsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;

    public WrapServiceExceptionsMiddleware(RequestDelegate next, IWebHostEnvironment env)
    {
        _next = next;
        _env = env;
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
    
    private Task HandleException(HttpContext context, Exception exception, string code, string message)
    {
        object obj = new
        {
            IsError = true, 
            Code = code,
            message = message
        };

        if (_env.IsDevelopment())
        {
            obj = new
            {
                IsError = true,
                Code = code,
                Message = message,
                ExceptionType = exception.GetType(),
                Source = exception.Source,
                Exception = exception
            };
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(JsonConvert.SerializeObject(obj));
    }

}
