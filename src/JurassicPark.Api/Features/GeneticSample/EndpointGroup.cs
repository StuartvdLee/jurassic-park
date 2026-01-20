using JurassicPark.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace JurassicPark.Api.Features.GeneticSample;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/geneticsamples");

        group.MapGet("", async (JurassicParkDbContext db) =>
            await db.GeneticSamples.ToListAsync());

        group.MapGet("/{id:int}", async (int id, JurassicParkDbContext db) =>
            await db.GeneticSamples.FindAsync(id) is var gs && gs != null
                ? Results.Ok(gs)
                : Results.NotFound());
    }
}
