using JurassicPark.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace JurassicPark.Api.Features.Dinosaur;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/dinosaurs");

        group.MapGet("", async (JurassicParkDbContext db) =>
            await db.Dinosaurs.ToListAsync());

        group.MapGet("/{id:int}", async (int id, JurassicParkDbContext db) =>
            await db.Dinosaurs.FindAsync(id) is var d && d != null
                ? Results.Ok(d)
                : Results.NotFound());
    }
}
