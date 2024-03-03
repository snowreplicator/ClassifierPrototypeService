using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Prototype.ClassifierPrototypeService.Application.Common;

public abstract record BaseRequest
{
    [Required]
    [DefaultValue("ru-RU")]
    public string Locale { get; set; }
}
