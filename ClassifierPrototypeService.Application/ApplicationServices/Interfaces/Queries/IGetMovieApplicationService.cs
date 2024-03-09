using Prototype.ClassifierPrototypeService.Application.Common;
using Prototype.ClassifierPrototypeService.Application.RequestModels.Queries;
using Prototype.ClassifierPrototypeService.Application.ViewModels.Movie;

namespace Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Queries;

public interface IGetMovieApplicationService : IApplicationService<GetMovieRequest, MovieViewModel>
{
}
