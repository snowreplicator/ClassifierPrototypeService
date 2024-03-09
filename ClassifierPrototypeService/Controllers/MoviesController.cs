using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Commands;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Queries;
using Prototype.ClassifierPrototypeService.Application.RequestModels;
using Prototype.ClassifierPrototypeService.Application.RequestModels.Commands;
using Prototype.ClassifierPrototypeService.Application.RequestModels.Queries;
using Prototype.ClassifierPrototypeService.Application.ViewModels.Movie;

namespace Prototype.ClassifierPrototypeService.Controllers;

public class MoviesController : BaseController
{

    /// <summary>
    /// get all movies
    /// </summary>
    [HttpGet("GetMovies")]
    [Description("Get all movies")]
    public async Task<ActionResult<IEnumerable<MovieViewModel>>> GetMoviesAsync()
    {
        IEnumerable<MovieViewModel> result = await GetApplicationService<IGetMoviesApplicationService>().HandleAsync(new EmptyRequest());
        return new ActionResult<IEnumerable<MovieViewModel>>(result);
    }
    
    /// <summary>
    /// get movie by id
    /// </summary>
    [HttpGet("GetMovie")]
    [Description("Get movie by id")]
    public async Task<ActionResult<MovieViewModel>> GetMovieAsync(int id)
    {
        MovieViewModel result = await GetApplicationService<IGetMovieApplicationService>().HandleAsync(new GetMovieRequest(id));
        return new ActionResult<MovieViewModel>(result);
    }
    
    /// <summary>
    /// add new movie 
    /// </summary>
    [HttpPost("AddMovie")]
    [Description("Add new movie")]
    public async Task<ActionResult<MovieViewModel>> AddMovieAsync(AddMovieRequest request)
    {
        MovieViewModel result = await GetApplicationService<IAddMovieApplicationService>().HandleAsync(request);
        return new ActionResult<MovieViewModel>(result);
    }
        
}
