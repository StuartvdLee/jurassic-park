namespace JurassicPark.Shared.Models;

public partial class FeedingSchedule
{
    public int FeedingId { get; set; }

    public int DinosaurId { get; set; }

    public DateTime FeedingTime { get; set; }

    public string FoodType { get; set; } = null!;

    public decimal QuantityKg { get; set; }

    public int? AssignedEmployeeId { get; set; }

    public string Status { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual Employee? AssignedEmployee { get; set; }

    public virtual Dinosaur Dinosaur { get; set; } = null!;
}
