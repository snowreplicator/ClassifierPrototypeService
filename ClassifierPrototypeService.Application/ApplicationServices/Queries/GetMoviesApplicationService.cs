using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Queries;
using Prototype.ClassifierPrototypeService.Application.Interfaces.QuerySources;
using Prototype.ClassifierPrototypeService.Application.RequestModels;
using Prototype.ClassifierPrototypeService.Application.ViewModels.Movie;

namespace Prototype.ClassifierPrototypeService.Application.ApplicationServices.Queries;

public class GetMoviesApplicationService : IGetMoviesApplicationService
{
    private readonly ILogger _logger;
    private readonly IMovieQuerySource _movieQuerySource;
    
    public GetMoviesApplicationService(ILogger<GetMoviesApplicationService> logger, IMovieQuerySource movieQuerySource)
    {
        _logger = logger;
        _movieQuerySource = movieQuerySource;
    }

    public async Task<IEnumerable<MovieViewModel>> HandleAsync(EmptyRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("--- get all movies start ---");
        IEnumerable<MovieViewModel> movies = await _movieQuerySource.GetMoviesAsync(cancellationToken);
        _logger.LogInformation("--- get all movies end ---");
        return movies;
    }
        
}
