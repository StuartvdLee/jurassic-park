using JurassicPark.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace JurassicPark.Api.Features.EmployeeRole;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/employeeroles");

        group.MapGet("", async (JurassicParkDbContext db) =>
            await db.EmployeeRoles.ToListAsync());

        group.MapGet("/{id:int}", async (int id, JurassicParkDbContext db) =>
            await db.EmployeeRoles.FindAsync(id) is var er && er != null
                ? Results.Ok(er)
                : Results.NotFound());
    }
}
