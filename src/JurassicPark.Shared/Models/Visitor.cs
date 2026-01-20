namespace JurassicPark.Shared.Models;

public partial class Visitor
{
    public int VisitorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Country { get; set; }

    public DateOnly? CheckInDate { get; set; }

    public DateOnly? CheckOutDate { get; set; }

    public string Status { get; set; } = null!;

    public string? TourPackage { get; set; }

    public virtual ICollection<TourParticipant> TourParticipants { get; set; } = new List<TourParticipant>();
}
