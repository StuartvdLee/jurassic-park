using ModelContextProtocol.Server;

namespace JurassicPark.Mcp.Remote.Resources;

[McpServerResourceType]
public class DinosaurResource
{
    [McpServerResource(Name = "DinosaurResource")]
    public string GetApiUri()
    {
        return "https://jurassicpark-api.azurewebsites.net";
    }
}
