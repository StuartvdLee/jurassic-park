namespace JurassicPark.Shared.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int RoleId { get; set; }

    public int? IslandId { get; set; }

    public DateOnly HireDate { get; set; }

    public DateOnly? TerminationDate { get; set; }

    public string Status { get; set; } = null!;

    public string? Email { get; set; }

    public int? ClearanceLevel { get; set; }

    public int? SupervisorId { get; set; }

    public virtual ICollection<FeedingSchedule> FeedingSchedules { get; set; } = new List<FeedingSchedule>();

    public virtual ICollection<Incident> Incidents { get; set; } = new List<Incident>();

    public virtual ICollection<Employee> InverseSupervisor { get; set; } = new List<Employee>();

    public virtual Island? Island { get; set; }

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual EmployeeRole Role { get; set; } = null!;

    public virtual Employee? Supervisor { get; set; }

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
