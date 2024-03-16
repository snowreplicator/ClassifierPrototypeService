using System.ComponentModel.DataAnnotations;

namespace Prototype.ClassifierPrototypeService.Infrastructure;

public class ServiceOptions
{
    [Required(ErrorMessage = "DB: Необходимо указать подключение к БД и схему")]
    public DbOptions Db { get; set; }
    
    [Required(ErrorMessage = "NativeLocale: Необходимо указать нативную локаль")]
    public string NativeLocale { get; set; }
    
    [Required(ErrorMessage = "UseSwaggerInProduction: использование сваггера в продакшн")]
    public bool UseSwaggerInProduction { get; set; }
}

public record DbOptions
{
    [Required(ErrorMessage = "DB:ConnectionString: Необходимо указать строку подключения к БД")]
    public string ConnectionString { get; set; }

    [Required(ErrorMessage = "DB:Schema: Необходимо указать схему")]
    public string Schema { get; set; }
}

