using System.ComponentModel.DataAnnotations;

namespace Prototype.ClassifierPrototypeService.Infrastructure;

public class ServiceOptions
{
    [Required(ErrorMessage = "DB: Необходимо указать подключение к БД и схему")]
    public DB DB { get; set; }
    
    [Required(ErrorMessage = "NativeLocale: Необходимо указать нативную локаль")]
    public string NativeLocale { get; set; }
}

public record DB
{
    [Required(ErrorMessage = "DB:ConnectionString: Необходимо указать строку подключения к БД")]
    public string ConnectionString { get; set; }

    [Required(ErrorMessage = "DB:Schema: Необходимо указать схему")]
    public string Schema { get; set; }
}
