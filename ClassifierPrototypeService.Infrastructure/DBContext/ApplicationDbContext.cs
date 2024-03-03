using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Prototype.ClassifierPrototypeService.Bll.Models;

namespace Prototype.ClassifierPrototypeService.Infrastructure.DBContext;

public sealed class ApplicationDbContext : DbContext
{
    private readonly DbContextOptions<ApplicationDbContext> _options;

    #region DB tables
    public DbSet<Movie> Movies => Set<Movie>();
    #endregion
    
    #region Helpers functions
    public IQueryable<TEntity> AsNoTracking<TEntity>() where TEntity : class 
        => Set<TEntity>().AsNoTracking();
    #endregion

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        _options = options;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(_options.FindExtension<DbContextSchemaUsedExtension>().Schema)
            .ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
