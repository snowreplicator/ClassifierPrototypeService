using Prototype.ClassifierPrototypeService.Application.Common;
using Prototype.ClassifierPrototypeService.Application.RequestModels.Commands;
using Prototype.ClassifierPrototypeService.Application.ViewModels.Movie;

namespace Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Commands;

public interface IAddMovieApplicationService : IApplicationService<AddMovieRequest, MovieViewModel>
{
}
