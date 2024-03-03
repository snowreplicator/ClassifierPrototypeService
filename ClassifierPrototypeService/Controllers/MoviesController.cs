using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Queries;
using Prototype.ClassifierPrototypeService.Application.RequestModels;
using Prototype.ClassifierPrototypeService.Application.RequestModels.Commands;
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
    /// add new movie 
    /// </summary>
    [HttpPost("AddMovie")]
    [Description("Add new movie")]
    public async Task<ActionResult<int>> AddMovieAsync(AddMovieRequest request)
    {
        int result = 0;
        return new ActionResult<int>(result);
    }
        
}
