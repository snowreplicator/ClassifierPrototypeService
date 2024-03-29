﻿using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Prototype.ClassifierPrototypeService.Application.Common;
using Prototype.ClassifierPrototypeService.Application.Interfaces.Repositories;
using Prototype.ClassifierPrototypeService.Bll.Common;
using Prototype.ClassifierPrototypeService.Bll.Models;
using Prototype.ClassifierPrototypeService.Infrastructure.common;

namespace Prototype.ClassifierPrototypeService.Infrastructure.Repositories;

public class MovieRepository : BaseRepository, IMovieRepository
{
    public MovieRepository(IRequestContext requestContext) : base(requestContext)
    {
    }
    
    public Task<Movie> this[int entityId] => GetByIdAsync(entityId);

    private async Task<Movie> FindByIdAsync(int id)
    {
        Movie movie = await RequestContext.ApplicationDbContext.Movies.FindAsync(id);
        return movie;
    }
    
    private async Task<Movie> GetByIdAsync(int id)
    {
        Movie movie = await FindByIdAsync(id);
        return movie ?? throw new InfrastructureLayerException($"Movie is not found by id '{id}'", Error.O100MovieNotFound);
    }

    public async Task<Movie> AddAsync(Movie movie)
    {
        EntityEntry<Movie> addedMovie = await RequestContext.ApplicationDbContext.Movies.AddAsync(movie);
        await RequestContext.ApplicationDbContext.SaveChangesAsync(); 
        return addedMovie.Entity;
    }

    public async Task DeleteAsync(int id)
    {
        Movie movie = await FindByIdAsync(id);
        if (movie is not null)
            RequestContext.ApplicationDbContext.Movies.Remove(movie);
    }
}
