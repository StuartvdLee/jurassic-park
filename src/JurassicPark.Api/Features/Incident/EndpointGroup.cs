using JurassicPark.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace JurassicPark.Api.Features.Incident;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/incidents");

        group.MapGet("", async (JurassicParkDbContext db) =>
            await db.Incidents.ToListAsync());

        group.MapGet("/{id:int}", async (int id, JurassicParkDbContext db) =>
            await db.Incidents.FindAsync(id) is var i && i != null
                ? Results.Ok(i)
                : Results.NotFound());
    }
}
