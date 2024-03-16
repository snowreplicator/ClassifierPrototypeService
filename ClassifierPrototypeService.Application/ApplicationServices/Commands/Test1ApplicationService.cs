using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Commands;
using Prototype.ClassifierPrototypeService.Application.Common;
using Prototype.ClassifierPrototypeService.Application.RequestModels;
using Prototype.ClassifierPrototypeService.Bll.Common;


namespace Prototype.ClassifierPrototypeService.Application.ApplicationServices.Commands;

public class Test1ApplicationService : BaseApplicationService<EmptyRequest, EmptyResult>, ITest1ApplicationService
{
    private readonly ILogger _logger;

    public Test1ApplicationService(ILogger<Test1ApplicationService> logger, IRequestContext requestContext) : base(logger, requestContext)
    {
        _logger = logger;
    }
    
    protected override Task<EmptyResult> ProtectedHandle(EmptyRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("--- start test 1  operation ---");
        throw new ApplicationLayerException("test 1 error message", "test1.error_code");
    }
    
    protected override ApplicationLayerException WrapException(Exception exception) 
        => new($"Unexpected error on test 1 application service: {exception.Message}", Error.O104Test1UnexpectedError);
}
