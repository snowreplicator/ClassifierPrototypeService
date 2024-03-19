using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Commands;
using Prototype.ClassifierPrototypeService.Application.RequestModels;

namespace Prototype.ClassifierPrototypeService.Controllers;

public class TestController : BaseController
{
    private readonly IWebHostEnvironment _env;

    public TestController(IWebHostEnvironment env)
    {
        _env = env;
    }
    
    /// <summary>
    /// test1
    /// </summary>
    [HttpGet("Test1")]
    public async Task<EmptyResult> Test1Async()
    {
        await GetApplicationService<ITest1ApplicationService>().HandleAsync(new EmptyRequest());
        return new EmptyResult();
    }
    
    /// <summary>
    /// test2
    /// </summary>
    [HttpGet("Test2")]
    public async Task<EmptyResult> Test2Async()
    {
        await GetApplicationService<ITest2ApplicationService>().HandleAsync(new EmptyRequest());
        return new EmptyResult();
    }
    
    /// <summary>
    /// test3
    /// </summary>
    [HttpGet("Test3")]
    public object Test3()
    {
        object obj = new
        {
            environment = _env.EnvironmentName 
        };
        
        return new ActionResult<object>(obj);
    }

}
