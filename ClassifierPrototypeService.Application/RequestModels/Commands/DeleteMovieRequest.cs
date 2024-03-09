using Prototype.ClassifierPrototypeService.Application.Common;

namespace Prototype.ClassifierPrototypeService.Application.RequestModels.Commands;

public sealed record DeleteMovieRequest : BaseRequest
{
    public int Id { get; set; }

    public DeleteMovieRequest(int id)
    {
        Id = id;
    }
}
