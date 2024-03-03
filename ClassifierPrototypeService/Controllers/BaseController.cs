using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prototype.ClassifierPrototypeService.Infrastructure;

namespace Prototype.ClassifierPrototypeService.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{
    protected TApplicationService GetApplicationService<TApplicationService>() where TApplicationService : class 
        => HttpContext.RequestServices.GetRequiredService<TApplicationService>();

    protected bool IsNativeLocale(string locale)
    {
        string native = GetNativeLocale();
        return locale == native;
    }

    private string GetNativeLocale()
    {
        string value = HttpContext.RequestServices
            .GetRequiredService<IConfiguration>()
            .Get<ServiceOptions>()
            .NativeLocale;

        return value;
    }
}
