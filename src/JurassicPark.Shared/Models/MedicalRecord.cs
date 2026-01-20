namespace JurassicPark.Shared.Models;

public partial class MedicalRecord
{
    public int RecordId { get; set; }

    public int DinosaurId { get; set; }

    public DateOnly CheckupDate { get; set; }

    public int? VeterinarianEmployeeId { get; set; }

    public decimal? WeightKg { get; set; }

    public decimal? TemperatureC { get; set; }

    public string? DiagnosisNotes { get; set; }

    public string? Treatment { get; set; }

    public DateOnly? NextCheckupDate { get; set; }

    public virtual Dinosaur Dinosaur { get; set; } = null!;

    public virtual Employee? VeterinarianEmployee { get; set; }
}
