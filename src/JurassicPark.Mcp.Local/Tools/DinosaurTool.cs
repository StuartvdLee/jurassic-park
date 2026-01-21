using System.ComponentModel;
using JurassicPark.Shared.Data;
using JurassicPark.Shared.Models;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;

namespace JurassicPark.Mcp.Local.Tools;

[McpServerToolType]
public static class DinosaurTool
{
    [McpServerTool(Name = "GetDinosaurs")]
    [Description("Retrieves a list of dinosaurs. Optionally takes a limit of the number of dinosaurs to retrieve.")]
    public static async Task<List<Dinosaur>> GetDinosaurs(JurassicParkDbContext dbContext, int? limit = null)
    {
        var query = dbContext.Dinosaurs.AsQueryable();

        if (limit is > 0) query = query.Take(limit.Value);

        return await query.ToListAsync();
    }
}
