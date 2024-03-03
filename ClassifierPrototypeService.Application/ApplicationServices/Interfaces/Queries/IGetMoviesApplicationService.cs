using System.Collections.Generic;
using Prototype.ClassifierPrototypeService.Application.Common;
using Prototype.ClassifierPrototypeService.Application.RequestModels;
using Prototype.ClassifierPrototypeService.Application.ViewModels.Movie;

namespace Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Queries;

public interface IGetMoviesApplicationService : IApplicationService<EmptyRequest, IEnumerable<MovieViewModel>>
{
}
