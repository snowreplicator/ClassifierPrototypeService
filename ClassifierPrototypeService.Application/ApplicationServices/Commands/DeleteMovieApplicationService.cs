using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Commands;
using Prototype.ClassifierPrototypeService.Application.Common;
using Prototype.ClassifierPrototypeService.Application.Interfaces.Repositories;
using Prototype.ClassifierPrototypeService.Application.RequestModels.Commands;
using Prototype.ClassifierPrototypeService.Bll.Common;

namespace Prototype.ClassifierPrototypeService.Application.ApplicationServices.Commands;

public class DeleteMovieApplicationService : BaseApplicationService<DeleteMovieRequest, EmptyResult>, IDeleteMovieApplicationService
{
    private readonly ILogger _logger;
    private readonly IMovieRepository _movieRepository;

    public DeleteMovieApplicationService(ILogger<DeleteMovieApplicationService> logger,
        IMovieRepository movieRepository,
        IRequestContext requestContext)
        : base(logger, requestContext)
    {
        _logger = logger;
        _movieRepository = movieRepository;
    }

    protected override async Task<EmptyResult> ProtectedHandle(DeleteMovieRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("--- start delete movie operation ---");

        await _movieRepository.DeleteAsync(request.Id);
        
        _logger.LogInformation("--- end delete operation (id: {}) ---", request.Id);
        return new EmptyResult();
    }
    
    protected override ApplicationLayerException WrapException(Exception exception) =>
        new($"Unexpected error on delete Movie: {exception.Message}", Error.O103MovieCouldNotBeDeleted);
}
