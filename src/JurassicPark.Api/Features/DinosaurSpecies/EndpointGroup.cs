using JurassicPark.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace JurassicPark.Api.Features.DinosaurSpecies;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/dinosaurspecies");

        group.MapGet("", async (JurassicParkDbContext db) =>
            await db.DinosaurSpecies.ToListAsync());

        group.MapGet("/{id:int}", async (int id, JurassicParkDbContext db) =>
            await db.DinosaurSpecies.FindAsync(id) is var ds && ds != null
                ? Results.Ok(ds)
                : Results.NotFound());
    }
}
