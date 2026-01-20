using JurassicPark.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace JurassicPark.Api.Features.Visitor;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/visitors");

        group.MapGet("", async (JurassicParkDbContext db) =>
            await db.Visitors.ToListAsync());

        group.MapGet("/{id:int}", async (int id, JurassicParkDbContext db) =>
            await db.Visitors.FindAsync(id) is var v && v != null
                ? Results.Ok(v)
                : Results.NotFound());
    }
}
