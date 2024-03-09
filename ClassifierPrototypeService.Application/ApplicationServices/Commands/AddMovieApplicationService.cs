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

public class AddMovieApplicationService : BaseApplicationService<AddMovieRequest, MovieViewModel>, IAddMovieApplicationService
{
    private readonly ILogger _logger;
    private readonly IMovieRepository _movieRepository;

    public AddMovieApplicationService(ILogger<AddMovieApplicationService> logger,
        IMovieRepository movieRepository,
        IRequestContext requestContext)
        : base(logger, requestContext)
    {
        _logger = logger;
        _movieRepository = movieRepository;
    }

    protected override async Task<MovieViewModel> ProtectedHandle(AddMovieRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("--- start add movie operation ---");

        var movie = Movie.CreateMovie(request.Title, request.Genre, request.ReleaseDate);
        movie = await _movieRepository.AddAsync(movie);
        
        _logger.LogInformation("--- end add movie operation (id: {MovieId}) ---", movie.Id);
        return MovieViewModel.FromDomain(movie);
    }
    
    protected override ApplicationLayerException WrapException(Exception exception) =>
        new($"Unexpected error on create Movie: {exception.Message}", Error.O101MovieCouldNotBeCreated);
}
