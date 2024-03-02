using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prototype.ClassifierPrototypeService.Bll.Models;

namespace Prototype.ClassifierPrototypeService.Infrastructure.DBContext.EntityTypeConfigurations;

internal class MovieEntityTypeConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("movie");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.Title).HasColumnName("title");
        builder.Property(a => a.Genre).HasColumnName("genre");
        builder.Property(a => a.ReleaseDate).HasColumnName("releasedate").HasColumnType("timestamp without time zone");
    }
}

