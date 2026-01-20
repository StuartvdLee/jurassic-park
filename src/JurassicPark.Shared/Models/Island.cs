namespace JurassicPark.Shared.Models;

public partial class Island
{
    public int IslandId { get; set; }

    public string IslandCode { get; set; } = null!;

    public string IslandName { get; set; } = null!;

    public string? Location { get; set; }

    public DateOnly? Established { get; set; }

    public DateOnly? Decommissioned { get; set; }

    public bool IsActive { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Facility> Facilities { get; set; } = new List<Facility>();

    public virtual ICollection<Incident> Incidents { get; set; } = new List<Incident>();

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
