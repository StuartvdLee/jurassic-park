namespace JurassicPark.Shared.Models;

public partial class DinosaurSpecies
{
    public int SpeciesId { get; set; }

    public string SpeciesCode { get; set; } = null!;

    public string CommonName { get; set; } = null!;

    public string ScientificName { get; set; } = null!;

    public string Period { get; set; } = null!;

    public string DietType { get; set; } = null!;

    public decimal? MaxHeightMeters { get; set; }

    public decimal? MaxLengthMeters { get; set; }

    public decimal? MaxWeightKg { get; set; }

    public int DangerLevel { get; set; }

    public int? IntelligenceLevel { get; set; }

    public string? SocialBehavior { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Dinosaur> Dinosaurs { get; set; } = new List<Dinosaur>();

    public virtual ICollection<GeneticSample> GeneticSamples { get; set; } = new List<GeneticSample>();
}
