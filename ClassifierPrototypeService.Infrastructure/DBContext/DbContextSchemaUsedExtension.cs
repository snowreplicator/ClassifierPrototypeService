using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Prototype.ClassifierPrototypeService.Infrastructure.DBContext;

internal class DbContextSchemaUsedExtension : IDbContextOptionsExtension
{
    public DbContextSchemaUsedExtension(string schema)
    {
        if (string.IsNullOrWhiteSpace(schema))
            throw new ArgumentNullException(nameof(schema), "Unable to use empty Schema!");

        Schema = schema;
    }

    public string Schema { get; }

    public void ApplyServices(IServiceCollection services)
    {
    }

    public void Validate(IDbContextOptions options)
    {
        if (string.IsNullOrWhiteSpace(Schema))
            throw new ArgumentNullException(nameof(Schema), "Unable to use empty Schema!");
    }

    private DbContextOptionsExtensionInfo _info;
    public DbContextOptionsExtensionInfo Info => _info ??= new ExtensionInfo(this);

    private sealed class ExtensionInfo : DbContextOptionsExtensionInfo
    {
        private new DbContextSchemaUsedExtension Extension => (DbContextSchemaUsedExtension)base.Extension;

        public ExtensionInfo(IDbContextOptionsExtension extension) : base(extension)
        {
        }

        public override int GetServiceProviderHashCode()
            => Extension.Schema.GetHashCode();

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo) 
            => debugInfo["Schema:UseCustomSchema"] = Extension.Schema.GetHashCode().ToString(CultureInfo.InvariantCulture);

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other) 
            => true;

        public override bool IsDatabaseProvider { get; }
        public override string LogFragment => $"(SchemaUsed:{Extension.Schema}) ";
    }
}
