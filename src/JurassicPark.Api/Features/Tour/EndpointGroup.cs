using JurassicPark.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace JurassicPark.Api.Features.Tour;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/tours");

        group.MapGet("", async (JurassicParkDbContext db) =>
            await db.Tours.ToListAsync());

        group.MapGet("/{id:int}", async (int id, JurassicParkDbContext db) =>
            await db.Tours.FindAsync(id) is var t && t != null
                ? Results.Ok(t)
                : Results.NotFound());
    }
}
