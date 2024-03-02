using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Prototype.ClassifierPrototypeService.Infrastructure.DBContext;

internal static class DbContextOptionsBuilderExtensions
{
    internal static DbContextOptionsBuilder UseSchema(this DbContextOptionsBuilder optionsBuilder, string schema)
    {
        if (string.IsNullOrWhiteSpace(schema))
            throw new ArgumentNullException(nameof(schema), "Unable to use empty Schema!");

        DbContextSchemaUsedExtension extension = optionsBuilder.Options.FindExtension<DbContextSchemaUsedExtension>();

        if (extension is not null && schema == extension.Schema)
            return optionsBuilder;

        extension = new DbContextSchemaUsedExtension(schema);

        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);
        return optionsBuilder;
    }
}
