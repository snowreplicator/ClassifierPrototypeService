using System;
using System.Threading.Tasks;
using Prototype.ClassifierPrototypeService.Bll.Models;

namespace Prototype.ClassifierPrototypeService.Application.Interfaces.Repositories;

public interface IMovieRepository
{
    Task<Movie> this[int entityId] { get; }
    Task<Movie> AddAsync(Movie movie);
    Task DeleteAsync(int id);
}
