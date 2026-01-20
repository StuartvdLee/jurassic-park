using JurassicPark.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace JurassicPark.Api.Features.Island;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/islands");

        group.MapGet("", async (JurassicParkDbContext db) =>
            await db.Islands.ToListAsync());

        group.MapGet("/{id:int}", async (int id, JurassicParkDbContext db) =>
            await db.Islands.FindAsync(id) is var i && i != null
                ? Results.Ok(i)
                : Results.NotFound());
    }
}
