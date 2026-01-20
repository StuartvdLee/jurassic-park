namespace JurassicPark.Shared.Models;

public partial class Dinosaur
{
    public int DinosaurId { get; set; }

    public int SpeciesId { get; set; }

    public string TagNumber { get; set; } = null!;

    public string? NickName { get; set; }

    public string Sex { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public DateOnly? DeathDate { get; set; }

    public string Status { get; set; } = null!;

    public int? CurrentPaddockId { get; set; }

    public string? Version { get; set; }

    public string? HealthStatus { get; set; }

    public DateOnly? LastCheckupDate { get; set; }

    public virtual Facility? CurrentPaddock { get; set; }

    public virtual ICollection<FeedingSchedule> FeedingSchedules { get; set; } = new List<FeedingSchedule>();

    public virtual ICollection<Incident> Incidents { get; set; } = new List<Incident>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual DinosaurSpecies Species { get; set; } = null!;
}
