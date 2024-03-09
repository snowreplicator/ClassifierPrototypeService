using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Queries;
using Prototype.ClassifierPrototypeService.Application.Interfaces.QuerySources;
using Prototype.ClassifierPrototypeService.Application.RequestModels.Queries;
using Prototype.ClassifierPrototypeService.Application.ViewModels.Movie;

namespace Prototype.ClassifierPrototypeService.Application.ApplicationServices.Queries;

public class GetMovieApplicationService : IGetMovieApplicationService
{
    private readonly ILogger _logger;
    private readonly IMovieQuerySource _movieQuerySource;
    
    public GetMovieApplicationService(ILogger<GetMovieApplicationService> logger, IMovieQuerySource movieQuerySource)
    {
        _logger = logger;
        _movieQuerySource = movieQuerySource;
    }

    public async Task<MovieViewModel> HandleAsync(GetMovieRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("get movie by id: {}", request.Id);
        MovieViewModel movie = await _movieQuerySource.GetMovieAsync(request.Id, cancellationToken);
        _logger.LogInformation("movie: {}", movie);
        return movie;
    }

}
