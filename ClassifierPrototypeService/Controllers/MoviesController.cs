using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prototype.ClassifierPrototypeService.Application.ApplicationServices.Interfaces.Queries;
using Prototype.ClassifierPrototypeService.Application.RequestModels;
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
        
}
