using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Prototype.ClassifierPrototypeService.Application.Common;
using Prototype.ClassifierPrototypeService.Application.Interfaces.Repositories;
using Prototype.ClassifierPrototypeService.Bll.Models;

namespace Prototype.ClassifierPrototypeService.Infrastructure.Repositories;

public class MovieRepository : BaseRepository, IMovieRepository
{
    public MovieRepository(IRequestContext requestContext) : base(requestContext)
    {
    }

    public async Task<Movie> AddAsync(Movie movie)
    {
        EntityEntry<Movie> addedMovie = await RequestContext.ApplicationDbContext.Movies.AddAsync(movie);
        await RequestContext.ApplicationDbContext.SaveChangesAsync(); 
        return addedMovie.Entity;
    }
}
