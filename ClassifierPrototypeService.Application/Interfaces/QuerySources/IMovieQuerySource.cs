using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Prototype.ClassifierPrototypeService.Application.ViewModels.Movie;

namespace Prototype.ClassifierPrototypeService.Application.Interfaces.QuerySources;

public interface IMovieQuerySource
{
    Task<IEnumerable<MovieViewModel>> GetMoviesAsync(CancellationToken cancellationToken);
    Task<MovieViewModel> FindMovieAsync(int id, CancellationToken cancellationToken);
    Task<MovieViewModel> GetMovieAsync(int id, CancellationToken cancellationToken);
}
