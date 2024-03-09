using Prototype.ClassifierPrototypeService.Application.Common;

namespace Prototype.ClassifierPrototypeService.Application.RequestModels.Queries;

public sealed record GetMovieRequest : BaseRequest
{
    public int Id { get; set; }

    public GetMovieRequest(int id)
    {
        Id = id;
    }
}
