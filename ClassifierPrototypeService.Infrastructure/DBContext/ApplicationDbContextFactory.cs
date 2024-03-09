using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

namespace Prototype.ClassifierPrototypeService.Infrastructure.DBContext;

// миграции
// создать: dotnet ef migrations add [MigrationName]
// посмотреть SQL: dotnet ef migrations script
// применить в БД: dotnet ef database update
// установить/проапдейтить инструмент: dotnet tool install --global dotnet-ef / dotnet tool update --global dotnet-ef 
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var configBuilder = new ConfigurationBuilder();
        string configFilePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "..",
            "ClassifierPrototypeService",
            "appsettings.Development.json");
        configBuilder.AddJsonFile(configFilePath);

        var dbOptions = new DbOptions();
        IConfigurationRoot config = configBuilder.Build();
        config.Bind("DB", dbOptions);

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder
            .UseNpgsql(dbOptions.ConnectionString, optionsAction => optionsAction.MigrationsHistoryTable(HistoryRepository.DefaultTableName, dbOptions.Schema))
            .UseSchema(dbOptions.Schema);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
