namespace JurassicPark.Shared.Models;

public partial class TourParticipant
{
    public int ParticipantId { get; set; }

    public int TourId { get; set; }

    public int VisitorId { get; set; }

    public bool WaiverSigned { get; set; }

    public int? SeatNumber { get; set; }

    public virtual Tour Tour { get; set; } = null!;

    public virtual Visitor Visitor { get; set; } = null!;
}
