namespace JurassicPark.Shared.Models;

public partial class Tour
{
    public int TourId { get; set; }

    public DateTime TourDate { get; set; }

    public string TourType { get; set; } = null!;

    public int? GuideEmployeeId { get; set; }

    public int? VehicleId { get; set; }

    public int? Capacity { get; set; }

    public int IslandId { get; set; }

    public string Status { get; set; } = null!;

    public virtual Employee? GuideEmployee { get; set; }

    public virtual Island Island { get; set; } = null!;

    public virtual ICollection<TourParticipant> TourParticipants { get; set; } = new List<TourParticipant>();

    public virtual Vehicle? Vehicle { get; set; }
}
