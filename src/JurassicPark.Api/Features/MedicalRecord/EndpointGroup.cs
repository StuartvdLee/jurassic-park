using JurassicPark.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace JurassicPark.Api.Features.MedicalRecord;

public class EndpointGroup : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/medicalrecords");

        group.MapGet("", async (JurassicParkDbContext db) =>
            await db.MedicalRecords.ToListAsync());

        group.MapGet("/{id:int}", async (int id, JurassicParkDbContext db) =>
            await db.MedicalRecords.FindAsync(id) is var mr && mr != null
                ? Results.Ok(mr)
                : Results.NotFound());
    }
}
