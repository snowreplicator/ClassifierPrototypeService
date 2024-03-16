using Microsoft.AspNetCore.Mvc;
using Prototype.ClassifierPrototypeService.Application.Common;
using Prototype.ClassifierPrototypeService.Application.RequestModels;

namespace Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Commands;

public interface ITest1ApplicationService : IApplicationService<EmptyRequest, EmptyResult>
{
}
