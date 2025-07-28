using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using PuzzleSolver.NumbersGame;

namespace PuzzleSolver.MCPServer.Resources;

[McpServerResourceType]
public static class NumbersResource
{
    [McpServerResource(MimeType = "application/json", Name = "numbers", Title = "Available Numbers", UriTemplate = "numbers://category/{category}")]
    [Description("A template resource describing the numbers available and their usage constraints for a given category.")]
    public static TextResourceContents Numbers(RequestContext<ReadResourceRequestParams> requestContext, string category)
    {
        if (!Enum.TryParse<NumberCategory>(category, true, out var numberCategory))
            throw new NotSupportedException($"Unknown resource: {requestContext.Params?.Uri}"); 

        var numbers = new Numbers(
            numberCategory,
            Number.FromCategory(numberCategory).Select(num => num.Value).Order().ToArray(),
            BoardRules.ReuseLimit(NumberCategory.Small));
        return new TextResourceContents
        {
            Uri = requestContext.Params?.Uri!,
            MimeType = "application/json",
            Text = JsonSerializer.Serialize(numbers)
        };
    }
}

public record Numbers(
    [property: JsonPropertyName("category")] NumberCategory Category, 
    [property: JsonPropertyName("options")] int[] Options,
    [property: JsonPropertyName("reuseLimit")] int ReuseLimit);
