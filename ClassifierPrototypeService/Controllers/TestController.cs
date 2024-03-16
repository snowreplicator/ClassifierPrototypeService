using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Commands;
using Prototype.ClassifierPrototypeService.Application.RequestModels;

namespace Prototype.ClassifierPrototypeService.Controllers;

public class TestController : BaseController
{
    /// <summary>
    /// test1
    /// </summary>
    [HttpGet("Test1")]
    [Description("test 1")]
    public async Task<EmptyResult> Test1Async()
    {
        await GetApplicationService<ITest1ApplicationService>().HandleAsync(new EmptyRequest());
        return new EmptyResult();
    }
    
    /// <summary>
    /// test2
    /// </summary>
    [HttpGet("Test2")]
    [Description("test 2")]
    public async Task<EmptyResult> Test2Async()
    {
        await GetApplicationService<ITest2ApplicationService>().HandleAsync(new EmptyRequest());
        return new EmptyResult();
    }
}
