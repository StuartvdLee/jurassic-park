using System.ComponentModel;
using System.Text.Json;
using JurassicPark.Shared.Models;
using ModelContextProtocol.Server;

namespace JurassicPark.Mcp.Remote.Tools;

[McpServerToolType]
public class DinosaurTool(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    [McpServerTool(Name = "GetDinosaurs")]
    [Description("Retrieves a list of dinosaurs. Optionally takes a limit of the number of dinosaurs to return.")]
    public async Task<List<Dinosaur>> GetDinosaursAsync(int? limit = null)
    {
        var response = await _httpClient.GetAsync("https://jurassicpark-api.azurewebsites.net/dinosaurs");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var dinosaurs = JsonSerializer.Deserialize<List<Dinosaur>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (limit is > 0) dinosaurs = dinosaurs?.Take(limit.Value).ToList();

        return dinosaurs ?? new List<Dinosaur>();
    }
}
