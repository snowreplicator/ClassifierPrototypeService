using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Commands;
using Prototype.ClassifierPrototypeService.Application.Common;
using Prototype.ClassifierPrototypeService.Application.RequestModels.Commands;

namespace Prototype.ClassifierPrototypeService.Application.ApplicationServices.Commands;

public class AddMovieApplicationService : BaseApplicationService<AddMovieRequest, int>, IAddMovieApplicationService
{
    private readonly ILogger _logger;

    public AddMovieApplicationService(ILogger<AddMovieApplicationService> logger,
                                      IRequestContext requestContext)
        : base(logger, requestContext)
    {
        _logger = logger;
    }

    protected override async Task<int> ProtectedHandle(AddMovieRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("--- start add movie operation ---");
        throw new NotImplementedException();
        _logger.LogInformation("--- end add movie operation ---");
    }
}
