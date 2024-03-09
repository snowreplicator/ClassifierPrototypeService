using Microsoft.AspNetCore.Mvc;
using Prototype.ClassifierPrototypeService.Application.Common;
using Prototype.ClassifierPrototypeService.Application.RequestModels.Commands;

namespace Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Commands;

public interface IDeleteMovieApplicationService : IApplicationService<DeleteMovieRequest, EmptyResult>
{
}
