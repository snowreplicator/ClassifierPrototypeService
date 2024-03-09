using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Commands;
using Prototype.ClassifierPrototypeService.Application.Common;
using Prototype.ClassifierPrototypeService.Application.Interfaces.Repositories;
using Prototype.ClassifierPrototypeService.Application.RequestModels.Commands;
using Prototype.ClassifierPrototypeService.Application.ViewModels.Movie;
using Prototype.ClassifierPrototypeService.Bll.Common;
using Prototype.ClassifierPrototypeService.Bll.Models;

namespace Prototype.ClassifierPrototypeService.Application.ApplicationServices.Commands;

public class UpdateMovieApplicationService : BaseApplicationService<UpdateMovieRequest, MovieViewModel>, IUpdateMovieApplicationService
{
    private readonly ILogger _logger;
    private readonly IMovieRepository _movieRepository;

    public UpdateMovieApplicationService(ILogger<UpdateMovieApplicationService> logger,
        IMovieRepository movieRepository,
        IRequestContext requestContext)
        : base(logger, requestContext)
    {
        _logger = logger;
        _movieRepository = movieRepository;
    }

    protected override async Task<MovieViewModel> ProtectedHandle(UpdateMovieRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("--- start update movie operation ---");

        Movie movie = await _movieRepository[request.Id];
        movie.Title = request.Title;
        movie.Genre = request.Genre;
        movie.ReleaseDate = request.ReleaseDate;
        
        _logger.LogInformation("--- end update operation ({}) ---", movie);
        return MovieViewModel.FromDomain(movie);
    }
    
    protected override ApplicationLayerException WrapException(Exception exception) =>
        new($"Unexpected error on update Movie: {exception.Message}", Error.O102MovieCouldNotBeUpdated);
}
