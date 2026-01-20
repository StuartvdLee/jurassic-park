using JurassicPark.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace JurassicPark.Api.Features.Employee;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/employees");

        group.MapGet("", async (JurassicParkDbContext db) =>
            await db.Employees.ToListAsync());

        group.MapGet("/{id:int}", async (int id, JurassicParkDbContext db) =>
            await db.Employees.FindAsync(id) is var e && e != null
                ? Results.Ok(e)
                : Results.NotFound());
    }
}
