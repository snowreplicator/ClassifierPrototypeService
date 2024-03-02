using System;
using Microsoft.EntityFrameworkCore;
using Prototype.ClassifierPrototypeService.Bll.Models;

namespace Prototype.ClassifierPrototypeService.Infrastructure.DBContext;

public sealed class ApplicationDbContext : DbContext
{
    private readonly DbContextOptions<ApplicationDbContext> _options;

    public DbSet<Movie> Movies => Set<Movie>();

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
