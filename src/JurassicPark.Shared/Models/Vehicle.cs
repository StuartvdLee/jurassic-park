namespace JurassicPark.Shared.Models;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public string VehicleType { get; set; } = null!;

    public string ModelName { get; set; } = null!;

    public int? Capacity { get; set; }

    public int IslandId { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly? LastMaintenanceDate { get; set; }

    public int? AssignedFacilityId { get; set; }

    public virtual Facility? AssignedFacility { get; set; }

    public virtual Island Island { get; set; } = null!;

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
