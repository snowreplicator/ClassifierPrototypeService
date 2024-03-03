using System;

namespace Prototype.ClassifierPrototypeService.Application.ViewModels.Movie;

public record MovieViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public DateTime ReleaseDate { get; set; }

    public static MovieViewModel FromDomain(Bll.Models.Movie movie)
        => new() {Id = movie.Id, Title = movie.Title, Genre = movie.Genre, ReleaseDate = movie.ReleaseDate};
}
