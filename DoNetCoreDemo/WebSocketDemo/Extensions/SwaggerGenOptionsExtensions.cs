using System.Xml.XPath;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebSocketDemo.Extensions;

public static class SwaggerGenOptionsExtensions
{
    public static void TryIncludeXmlComments(
        this SwaggerGenOptions swaggerGenOptions,
        string filePath,
        bool includeControllerXmlComments = false)
    {
        if (File.Exists(filePath))
            swaggerGenOptions.IncludeXmlComments(() => new XPathDocument(filePath), includeControllerXmlComments);
    }
}