using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;

namespace Prototype.ClassifierPrototypeService.Configuration;

public static class JsonConfiguration
{
    private const string ApiDateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";

    public static void ConfigureJsonSerializer(MvcNewtonsoftJsonOptions options)
        => options.SerializerSettings.Converters = options.SerializerSettings.Converters
            .Append(new IsoDateTimeConverter() {DateTimeFormat = ApiDateTimeFormat})
            .ToList();
}
