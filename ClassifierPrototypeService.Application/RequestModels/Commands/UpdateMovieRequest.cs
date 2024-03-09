using System;
using Prototype.ClassifierPrototypeService.Application.Common;

namespace Prototype.ClassifierPrototypeService.Application.RequestModels.Commands;

public sealed record UpdateMovieRequest : BaseRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public DateTime ReleaseDate { get; set; }
}
