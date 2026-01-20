using JurassicPark.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace JurassicPark.Api.Features.Vehicle;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/vehicles");

        group.MapGet("", async (JurassicParkDbContext db) =>
            await db.Vehicles.ToListAsync());

        group.MapGet("/{id:int}", async (int id, JurassicParkDbContext db) =>
            await db.Vehicles.FindAsync(id) is var v && v != null
                ? Results.Ok(v)
                : Results.NotFound());
    }
}
