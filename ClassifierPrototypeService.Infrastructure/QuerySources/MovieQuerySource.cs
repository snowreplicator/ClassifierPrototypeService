using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Prototype.ClassifierPrototypeService.Application.Interfaces.QuerySources;
using Prototype.ClassifierPrototypeService.Application.ViewModels.Movie;
using Prototype.ClassifierPrototypeService.Bll.Common;
using Prototype.ClassifierPrototypeService.Bll.Models;
using Prototype.ClassifierPrototypeService.Infrastructure.common;
using Prototype.ClassifierPrototypeService.Infrastructure.DBContext;

namespace Prototype.ClassifierPrototypeService.Infrastructure.QuerySources;

public class MovieQuerySource : IMovieQuerySource
{
    private readonly ILogger _logger;
    private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

    public MovieQuerySource(ILogger<MovieQuerySource> logger,
        IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
    {
        _logger = logger;
        _applicationDbContextFactory = applicationDbContextFactory;
    }

    public async Task<IEnumerable<MovieViewModel>> GetMoviesAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("--- query source GetMoviesAsync start ---");
        await using ApplicationDbContext context =
            await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

        List<MovieViewModel> movies = await context
            .AsNoTracking<Movie>()
            .Select(item => MovieViewModel.FromDomain(item))
            .ToListAsync(cancellationToken);

        _logger.LogInformation("--- query source GetMoviesAsync end ---");
        return movies;
    }

    public async Task<MovieViewModel> FindMovieAsync(int id, CancellationToken cancellationToken)
    {
        await using ApplicationDbContext context = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);
        
        Movie movie = await context
            .AsNoTracking<Movie>()
            .FirstOrDefaultAsync(movie => movie.Id == id, cancellationToken: cancellationToken);
        
        return movie == null ? null : MovieViewModel.FromDomain(movie);
    }

    public async Task<MovieViewModel> GetMovieAsync(int id, CancellationToken cancellationToken)
    {
        MovieViewModel movieViewModel = await FindMovieAsync(id, cancellationToken);
        if (movieViewModel is null)
            throw new InfrastructureLayerException($"Movie is not found by id '{id}'", Error.O100MovieNotFound);
        return movieViewModel;
    }
}
