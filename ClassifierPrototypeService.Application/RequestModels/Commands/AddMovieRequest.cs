using System;
using Prototype.ClassifierPrototypeService.Application.Common;

namespace Prototype.ClassifierPrototypeService.Application.RequestModels.Commands;

public sealed record AddMovieRequest : BaseRequest
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public DateTime ReleaseDate { get; set; }
}
