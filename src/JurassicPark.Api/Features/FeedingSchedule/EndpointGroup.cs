using JurassicPark.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace JurassicPark.Api.Features.FeedingSchedule;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/feedingschedules");

        group.MapGet("", async (JurassicParkDbContext db) =>
            await db.FeedingSchedules.ToListAsync());

        group.MapGet("/{id:int}", async (int id, JurassicParkDbContext db) =>
            await db.FeedingSchedules.FindAsync(id) is var fs && fs != null
                ? Results.Ok(fs)
                : Results.NotFound());
    }
}
