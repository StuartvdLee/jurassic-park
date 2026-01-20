namespace JurassicPark.Shared.Models;

public partial class GeneticSample
{
    public int SampleId { get; set; }

    public int SpeciesId { get; set; }

    public string SourceType { get; set; } = null!;

    public DateOnly ExtractionDate { get; set; }

    public string? FoundLocation { get; set; }

    public decimal? ViabilityPercent { get; set; }

    public int? StorageFacilityId { get; set; }

    public string? Notes { get; set; }

    public virtual DinosaurSpecies Species { get; set; } = null!;

    public virtual Facility? StorageFacility { get; set; }
}
