using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Prototype.ClassifierPrototypeService.Application.Interfaces.QuerySources;
using Prototype.ClassifierPrototypeService.Application.ViewModels.Movie;
using Prototype.ClassifierPrototypeService.Bll.Models;
using Prototype.ClassifierPrototypeService.Infrastructure.DBContext;

namespace Prototype.ClassifierPrototypeService.Infrastructure.QuerySources;

public class MovieQuerySource : IMovieQuerySource
{
    private readonly ILogger _logger;
    private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

    public MovieQuerySource(ILogger<MovieQuerySource> logger, IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
    {
        _logger = logger;
        _applicationDbContextFactory = applicationDbContextFactory;
    }
    
    public async Task<IEnumerable<MovieViewModel>> GetMoviesAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("--- query source GetMoviesAsync start ---");
        await using ApplicationDbContext context = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);
        
        List<MovieViewModel> movies = await context
            .AsNoTracking<Movie>()
            .Select(item => MovieViewModel.FromDomain(item))
            .ToListAsync(cancellationToken);
        
        _logger.LogInformation("--- query source GetMoviesAsync end ---");
        return movies;
    }
}
