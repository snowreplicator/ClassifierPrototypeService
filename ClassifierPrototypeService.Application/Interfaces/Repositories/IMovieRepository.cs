using System.Threading.Tasks;
using Prototype.ClassifierPrototypeService.Bll.Models;

namespace Prototype.ClassifierPrototypeService.Application.Interfaces.Repositories;

public interface IMovieRepository
{
    Task<Movie> AddAsync(Movie movie);
}
