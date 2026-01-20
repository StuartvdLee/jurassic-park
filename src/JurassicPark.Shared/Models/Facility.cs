namespace JurassicPark.Shared.Models;

public partial class Facility
{
    public int FacilityId { get; set; }

    public int IslandId { get; set; }

    public string FacilityName { get; set; } = null!;

    public string FacilityType { get; set; } = null!;

    public int? Capacity { get; set; }

    public bool IsOperational { get; set; }

    public DateOnly? ConstructedDate { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Dinosaur> Dinosaurs { get; set; } = new List<Dinosaur>();

    public virtual ICollection<GeneticSample> GeneticSamples { get; set; } = new List<GeneticSample>();

    public virtual Island Island { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
