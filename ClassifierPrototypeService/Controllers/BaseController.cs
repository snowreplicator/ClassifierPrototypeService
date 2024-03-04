using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Prototype.ClassifierPrototypeService.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{
    protected TApplicationService GetApplicationService<TApplicationService>() where TApplicationService : class
    {
        TApplicationService applicationService = HttpContext.RequestServices.GetRequiredService<TApplicationService>();

        var property = applicationService.GetType().GetProperty("UserGuid");
        if (property is not null && property.PropertyType == typeof(Guid))
        {
            property.SetValue(applicationService, GetUserId());
        }

        return applicationService;
    }

    protected Guid? GetUserId()
    {
        var userId = User.FindFirst("provider_user_id");
        return userId != null ? new Guid(userId.Value) : null;
    }

}
