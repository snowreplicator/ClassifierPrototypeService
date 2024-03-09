using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            throw new ServiceException(ile.Message, ile.Code, ile);
        }
        catch (ApplicationLayerException ale)
        {
            throw new ServiceException(ale.Message, ale.Code, ale);
        }
        catch (BllLayerException ble)
        {
            throw new ServiceException(ble.Message, ble.Code, ble);
        }        
        catch (Exception e)
        {
            throw new ServiceException(e.Message, Error.S100ErrorHandlingRequest, e);
        }
    }
}
