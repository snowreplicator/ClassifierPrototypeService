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

public class Test2ApplicationService : BaseApplicationService<EmptyRequest, EmptyResult>, ITest2ApplicationService
{
    private readonly ILogger _logger;

    public Test2ApplicationService(ILogger<Test1ApplicationService> logger, IRequestContext requestContext) : base(logger, requestContext)
    {
        _logger = logger;
    }
    
    protected override Task<EmptyResult> ProtectedHandle(EmptyRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("--- start test 2  operation ---");
        int x = 1;
        int y = 0;
        int z = x / y;
        
        return Task.FromResult(new EmptyResult());
    }
    
    protected override ApplicationLayerException WrapException(Exception exception) 
        => new($"Unexpected error on test 2 application service: {exception.Message}", Error.O105Test2UnexpectedError);
}
