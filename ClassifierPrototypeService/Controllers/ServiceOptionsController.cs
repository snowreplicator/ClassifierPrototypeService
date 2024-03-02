using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Prototype.ClassifierPrototypeService.Infrastructure;

namespace Prototype.ClassifierPrototypeService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceOptionsController : ControllerBase
{
    private readonly ServiceOptions _serviceOptions;

    public ServiceOptionsController(IOptions<ServiceOptions> serviceOptions)
    {
        _serviceOptions = serviceOptions.Value;
    }

    /// <summary>
    /// Retrieve all service options.
    /// </summary>
    [HttpGet]
    public IActionResult GetServiceOptions()
    {
        return Ok(_serviceOptions);
    }
}
