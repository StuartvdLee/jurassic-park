using JurassicPark.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace JurassicPark.Api.Features.TourParticipant;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/tourparticipants");

        group.MapGet("", async (JurassicParkDbContext db) =>
            await db.TourParticipants.ToListAsync());

        group.MapGet("/{id:int}", async (int id, JurassicParkDbContext db) =>
            await db.TourParticipants.FindAsync(id) is var tp && tp != null
                ? Results.Ok(tp)
                : Results.NotFound());
    }
}
