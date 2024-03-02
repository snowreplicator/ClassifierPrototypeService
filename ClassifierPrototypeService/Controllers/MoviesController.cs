using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prototype.ClassifierPrototypeService.Bll.Models;
using Prototype.ClassifierPrototypeService.Infrastructure.DBContext;

namespace Prototype.ClassifierPrototypeService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;

    public MoviesController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    /// <summary>
    /// get all movies
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesAsync()
    {
        if (_applicationDbContext.Movies == null)
        {
            return NotFound();
        }

        return await _applicationDbContext.Movies.ToListAsync();
    }
}
