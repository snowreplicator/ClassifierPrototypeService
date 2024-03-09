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
    /// get movie by id (query param)
    /// </summary>
    [HttpGet("GetMovieQueryParam")]
    [Description("Get movie by id")]
    public async Task<ActionResult<MovieViewModel>> GetMovieQueryParamAsync(int id)
    {
        MovieViewModel result = await GetApplicationService<IGetMovieApplicationService>().HandleAsync(new GetMovieRequest(id));
        return new ActionResult<MovieViewModel>(result);
    }
    
    /// <summary>
    /// get movie by id path param
    /// </summary>
    [HttpGet("GetMoviePathParam/{id}")]
    [Description("Get movie by id")]
    public async Task<ActionResult<MovieViewModel>> GetMoviePathParamAsync(int id)
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
    
    /// <summary>
    /// update existing movie (id in object) 
    /// </summary>
    [HttpPut("UpdateMovieIdInObject")]
    [Description("update existing movie")]
    public async Task<ActionResult<MovieViewModel>> UpdateMovieIdInObjectAsync(UpdateMovieRequest request)
    {
        MovieViewModel result = await GetApplicationService<IUpdateMovieApplicationService>().HandleAsync(request);
        return new ActionResult<MovieViewModel>(result);
    }
    
    /// <summary>
    /// update existing movie (id as query) 
    /// </summary>
    [HttpPut("UpdateMovieIdInPath/{id}")]
    [Description("update existing movie")]
    public async Task<ActionResult<MovieViewModel>> UpdateMovieIdInPathAsync(int id, UpdateMovieRequest request)
    {
        request.Id = id;
        MovieViewModel result = await GetApplicationService<IUpdateMovieApplicationService>().HandleAsync(request);
        return new ActionResult<MovieViewModel>(result);
    }
        
}
