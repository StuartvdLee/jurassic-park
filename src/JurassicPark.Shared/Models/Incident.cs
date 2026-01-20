namespace JurassicPark.Shared.Models;

public partial class Incident
{
    public int IncidentId { get; set; }

    public DateTime IncidentDate { get; set; }

    public string IncidentType { get; set; } = null!;

    public int SeverityLevel { get; set; }

    public int IslandId { get; set; }

    public int? DinosaurId { get; set; }

    public string? Description { get; set; }

    public int? Casualties { get; set; }

    public int? Injuries { get; set; }

    public int? ReportedByEmployeeId { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly? ResolutionDate { get; set; }

    public virtual Dinosaur? Dinosaur { get; set; }

    public virtual Island Island { get; set; } = null!;

    public virtual Employee? ReportedByEmployee { get; set; }
}
