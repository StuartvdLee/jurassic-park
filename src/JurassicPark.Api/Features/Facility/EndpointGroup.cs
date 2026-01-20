using JurassicPark.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace JurassicPark.Api.Features.Facility;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/facilities");

        group.MapGet("", async (JurassicParkDbContext db) =>
            await db.Facilities.ToListAsync());

        group.MapGet("/{id:int}", async (int id, JurassicParkDbContext db) =>
            await db.Facilities.FindAsync(id) is var f && f != null
                ? Results.Ok(f)
                : Results.NotFound());
    }
}
