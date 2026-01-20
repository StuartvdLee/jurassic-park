using JurassicPark.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace JurassicPark.Shared.Data;

public partial class JurassicParkDbContext : DbContext
{
    public JurassicParkDbContext(DbContextOptions<JurassicParkDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dinosaur> Dinosaurs { get; set; }

    public virtual DbSet<DinosaurSpecies> DinosaurSpecies { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }

    public virtual DbSet<Facility> Facilities { get; set; }

    public virtual DbSet<FeedingSchedule> FeedingSchedules { get; set; }

    public virtual DbSet<GeneticSample> GeneticSamples { get; set; }

    public virtual DbSet<Incident> Incidents { get; set; }

    public virtual DbSet<Island> Islands { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<TourParticipant> TourParticipants { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<Visitor> Visitors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dinosaur>(entity =>
        {
            entity.HasKey(e => e.DinosaurId).HasName("dinosaurs_pkey");

            entity.ToTable("dinosaurs", "jurassic_park");

            entity.HasIndex(e => e.CurrentPaddockId, "idx_dinosaurs_paddock");

            entity.HasIndex(e => e.SpeciesId, "idx_dinosaurs_species");

            entity.HasIndex(e => e.TagNumber, "dinosaurs_tag_number_key").IsUnique();

            entity.Property(e => e.DinosaurId).HasColumnName("dinosaur_id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.CurrentPaddockId).HasColumnName("current_paddock_id");
            entity.Property(e => e.DeathDate).HasColumnName("death_date");
            entity.Property(e => e.HealthStatus)
                .HasMaxLength(100)
                .HasColumnName("health_status");
            entity.Property(e => e.LastCheckupDate).HasColumnName("last_checkup_date");
            entity.Property(e => e.NickName)
                .HasMaxLength(100)
                .HasColumnName("nick_name");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .HasColumnName("sex");
            entity.Property(e => e.SpeciesId).HasColumnName("species_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Alive'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.TagNumber)
                .HasMaxLength(50)
                .HasColumnName("tag_number");
            entity.Property(e => e.Version)
                .HasMaxLength(20)
                .HasColumnName("version");

            entity.HasOne(d => d.CurrentPaddock).WithMany(p => p.Dinosaurs)
                .HasForeignKey(d => d.CurrentPaddockId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("dinosaurs_current_paddock_id_fkey");

            entity.HasOne(d => d.Species).WithMany(p => p.Dinosaurs)
                .HasForeignKey(d => d.SpeciesId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("dinosaurs_species_id_fkey");
        });

        modelBuilder.Entity<DinosaurSpecies>(entity =>
        {
            entity.HasKey(e => e.SpeciesId).HasName("dinosaur_species_pkey");

            entity.ToTable("dinosaur_species", "jurassic_park");

            entity.HasIndex(e => e.SpeciesCode, "dinosaur_species_species_code_key").IsUnique();

            entity.Property(e => e.SpeciesId).HasColumnName("species_id");
            entity.Property(e => e.CommonName)
                .HasMaxLength(100)
                .HasColumnName("common_name");
            entity.Property(e => e.DangerLevel).HasColumnName("danger_level");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DietType)
                .HasMaxLength(50)
                .HasColumnName("diet_type");
            entity.Property(e => e.IntelligenceLevel).HasColumnName("intelligence_level");
            entity.Property(e => e.MaxHeightMeters)
                .HasPrecision(5, 2)
                .HasColumnName("max_height_meters");
            entity.Property(e => e.MaxLengthMeters)
                .HasPrecision(5, 2)
                .HasColumnName("max_length_meters");
            entity.Property(e => e.MaxWeightKg)
                .HasPrecision(8, 2)
                .HasColumnName("max_weight_kg");
            entity.Property(e => e.Period)
                .HasMaxLength(50)
                .HasColumnName("period");
            entity.Property(e => e.ScientificName)
                .HasMaxLength(200)
                .HasColumnName("scientific_name");
            entity.Property(e => e.SocialBehavior)
                .HasMaxLength(50)
                .HasColumnName("social_behavior");
            entity.Property(e => e.SpeciesCode)
                .HasMaxLength(20)
                .HasColumnName("species_code");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employees_pkey");

            entity.ToTable("employees", "jurassic_park");

            entity.HasIndex(e => e.IslandId, "idx_employees_island");

            entity.HasIndex(e => e.RoleId, "idx_employees_role");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.ClearanceLevel).HasColumnName("clearance_level");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.IslandId).HasColumnName("island_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Active'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.SupervisorId).HasColumnName("supervisor_id");
            entity.Property(e => e.TerminationDate).HasColumnName("termination_date");

            entity.HasOne(d => d.Island).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IslandId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("employees_island_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("employees_role_id_fkey");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.InverseSupervisor)
                .HasForeignKey(d => d.SupervisorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("employees_supervisor_id_fkey");
        });

        modelBuilder.Entity<EmployeeRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("employee_roles_pkey");

            entity.ToTable("employee_roles", "jurassic_park");

            entity.HasIndex(e => e.RoleCode, "employee_roles_role_code_key").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(100)
                .HasColumnName("department_name");
            entity.Property(e => e.RoleCode)
                .HasMaxLength(20)
                .HasColumnName("role_code");
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.HasKey(e => e.FacilityId).HasName("facilities_pkey");

            entity.ToTable("facilities", "jurassic_park");

            entity.HasIndex(e => e.IslandId, "idx_facilities_island");

            entity.Property(e => e.FacilityId).HasColumnName("facility_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.ConstructedDate).HasColumnName("constructed_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FacilityName)
                .HasMaxLength(200)
                .HasColumnName("facility_name");
            entity.Property(e => e.FacilityType)
                .HasMaxLength(50)
                .HasColumnName("facility_type");
            entity.Property(e => e.IslandId).HasColumnName("island_id");
            entity.Property(e => e.IsOperational)
                .HasDefaultValue(true)
                .HasColumnName("is_operational");

            entity.HasOne(d => d.Island).WithMany(p => p.Facilities)
                .HasForeignKey(d => d.IslandId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("facilities_island_id_fkey");
        });

        modelBuilder.Entity<FeedingSchedule>(entity =>
        {
            entity.HasKey(e => e.FeedingId).HasName("feeding_schedules_pkey");

            entity.ToTable("feeding_schedules", "jurassic_park");

            entity.HasIndex(e => e.DinosaurId, "idx_feeding_dinosaur");

            entity.Property(e => e.FeedingId).HasColumnName("feeding_id");
            entity.Property(e => e.AssignedEmployeeId).HasColumnName("assigned_employee_id");
            entity.Property(e => e.DinosaurId).HasColumnName("dinosaur_id");
            entity.Property(e => e.FeedingTime).HasColumnName("feeding_time");
            entity.Property(e => e.FoodType)
                .HasMaxLength(100)
                .HasColumnName("food_type");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.QuantityKg)
                .HasPrecision(8, 2)
                .HasColumnName("quantity_kg");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Pending'::character varying")
                .HasColumnName("status");

            entity.HasOne(d => d.AssignedEmployee).WithMany(p => p.FeedingSchedules)
                .HasForeignKey(d => d.AssignedEmployeeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("feeding_schedules_assigned_employee_id_fkey");

            entity.HasOne(d => d.Dinosaur).WithMany(p => p.FeedingSchedules)
                .HasForeignKey(d => d.DinosaurId)
                .HasConstraintName("feeding_schedules_dinosaur_id_fkey");
        });

        modelBuilder.Entity<GeneticSample>(entity =>
        {
            entity.HasKey(e => e.SampleId).HasName("genetic_samples_pkey");

            entity.ToTable("genetic_samples", "jurassic_park");

            entity.Property(e => e.SampleId).HasColumnName("sample_id");
            entity.Property(e => e.ExtractionDate).HasColumnName("extraction_date");
            entity.Property(e => e.FoundLocation)
                .HasMaxLength(200)
                .HasColumnName("found_location");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.SourceType)
                .HasMaxLength(100)
                .HasColumnName("source_type");
            entity.Property(e => e.SpeciesId).HasColumnName("species_id");
            entity.Property(e => e.StorageFacilityId).HasColumnName("storage_facility_id");
            entity.Property(e => e.ViabilityPercent)
                .HasPrecision(5, 2)
                .HasColumnName("viability_percent");

            entity.HasOne(d => d.Species).WithMany(p => p.GeneticSamples)
                .HasForeignKey(d => d.SpeciesId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("genetic_samples_species_id_fkey");

            entity.HasOne(d => d.StorageFacility).WithMany(p => p.GeneticSamples)
                .HasForeignKey(d => d.StorageFacilityId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("genetic_samples_storage_facility_id_fkey");
        });

        modelBuilder.Entity<Incident>(entity =>
        {
            entity.HasKey(e => e.IncidentId).HasName("incidents_pkey");

            entity.ToTable("incidents", "jurassic_park");

            entity.HasIndex(e => e.IncidentDate, "idx_incidents_date");

            entity.HasIndex(e => e.IslandId, "idx_incidents_island");

            entity.Property(e => e.IncidentId).HasColumnName("incident_id");
            entity.Property(e => e.Casualties)
                .HasDefaultValue(0)
                .HasColumnName("casualties");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DinosaurId).HasColumnName("dinosaur_id");
            entity.Property(e => e.IncidentDate).HasColumnName("incident_date");
            entity.Property(e => e.IncidentType)
                .HasMaxLength(50)
                .HasColumnName("incident_type");
            entity.Property(e => e.Injuries)
                .HasDefaultValue(0)
                .HasColumnName("injuries");
            entity.Property(e => e.IslandId).HasColumnName("island_id");
            entity.Property(e => e.ReportedByEmployeeId).HasColumnName("reported_by_employee_id");
            entity.Property(e => e.ResolutionDate).HasColumnName("resolution_date");
            entity.Property(e => e.SeverityLevel).HasColumnName("severity_level");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Under Investigation'::character varying")
                .HasColumnName("status");

            entity.HasOne(d => d.Dinosaur).WithMany(p => p.Incidents)
                .HasForeignKey(d => d.DinosaurId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("incidents_dinosaur_id_fkey");

            entity.HasOne(d => d.Island).WithMany(p => p.Incidents)
                .HasForeignKey(d => d.IslandId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("incidents_island_id_fkey");

            entity.HasOne(d => d.ReportedByEmployee).WithMany(p => p.Incidents)
                .HasForeignKey(d => d.ReportedByEmployeeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("incidents_reported_by_employee_id_fkey");
        });

        modelBuilder.Entity<Island>(entity =>
        {
            entity.HasKey(e => e.IslandId).HasName("islands_pkey");

            entity.ToTable("islands", "jurassic_park");

            entity.HasIndex(e => e.IslandCode, "islands_island_code_key").IsUnique();

            entity.Property(e => e.IslandId).HasColumnName("island_id");
            entity.Property(e => e.Decommissioned).HasColumnName("decommissioned");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Established).HasColumnName("established");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IslandCode)
                .HasMaxLength(20)
                .HasColumnName("island_code");
            entity.Property(e => e.IslandName)
                .HasMaxLength(100)
                .HasColumnName("island_name");
            entity.Property(e => e.Location)
                .HasMaxLength(200)
                .HasColumnName("location");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("medical_records_pkey");

            entity.ToTable("medical_records", "jurassic_park");

            entity.HasIndex(e => e.DinosaurId, "idx_medical_dinosaur");

            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.CheckupDate).HasColumnName("checkup_date");
            entity.Property(e => e.DiagnosisNotes).HasColumnName("diagnosis_notes");
            entity.Property(e => e.DinosaurId).HasColumnName("dinosaur_id");
            entity.Property(e => e.NextCheckupDate).HasColumnName("next_checkup_date");
            entity.Property(e => e.TemperatureC)
                .HasPrecision(5, 2)
                .HasColumnName("temperature_c");
            entity.Property(e => e.Treatment).HasColumnName("treatment");
            entity.Property(e => e.VeterinarianEmployeeId).HasColumnName("veterinarian_employee_id");
            entity.Property(e => e.WeightKg)
                .HasPrecision(8, 2)
                .HasColumnName("weight_kg");

            entity.HasOne(d => d.Dinosaur).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.DinosaurId)
                .HasConstraintName("medical_records_dinosaur_id_fkey");

            entity.HasOne(d => d.VeterinarianEmployee).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.VeterinarianEmployeeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("medical_records_veterinarian_employee_id_fkey");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.TourId).HasName("tours_pkey");

            entity.ToTable("tours", "jurassic_park");

            entity.HasIndex(e => e.TourDate, "idx_tours_date");

            entity.HasIndex(e => e.IslandId, "idx_tours_island");

            entity.Property(e => e.TourId).HasColumnName("tour_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.GuideEmployeeId).HasColumnName("guide_employee_id");
            entity.Property(e => e.IslandId).HasColumnName("island_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Scheduled'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.TourDate).HasColumnName("tour_date");
            entity.Property(e => e.TourType)
                .HasMaxLength(50)
                .HasColumnName("tour_type");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.GuideEmployee).WithMany(p => p.Tours)
                .HasForeignKey(d => d.GuideEmployeeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("tours_guide_employee_id_fkey");

            entity.HasOne(d => d.Island).WithMany(p => p.Tours)
                .HasForeignKey(d => d.IslandId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("tours_island_id_fkey");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Tours)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("tours_vehicle_id_fkey");
        });

        modelBuilder.Entity<TourParticipant>(entity =>
        {
            entity.HasKey(e => e.ParticipantId).HasName("tour_participants_pkey");

            entity.ToTable("tour_participants", "jurassic_park");

            entity.Property(e => e.ParticipantId).HasColumnName("participant_id");
            entity.Property(e => e.SeatNumber).HasColumnName("seat_number");
            entity.Property(e => e.TourId).HasColumnName("tour_id");
            entity.Property(e => e.VisitorId).HasColumnName("visitor_id");
            entity.Property(e => e.WaiverSigned)
                .HasDefaultValue(false)
                .HasColumnName("waiver_signed");

            entity.HasOne(d => d.Tour).WithMany(p => p.TourParticipants)
                .HasForeignKey(d => d.TourId)
                .HasConstraintName("tour_participants_tour_id_fkey");

            entity.HasOne(d => d.Visitor).WithMany(p => p.TourParticipants)
                .HasForeignKey(d => d.VisitorId)
                .HasConstraintName("tour_participants_visitor_id_fkey");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("vehicles_pkey");

            entity.ToTable("vehicles", "jurassic_park");

            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
            entity.Property(e => e.AssignedFacilityId).HasColumnName("assigned_facility_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.IslandId).HasColumnName("island_id");
            entity.Property(e => e.LastMaintenanceDate).HasColumnName("last_maintenance_date");
            entity.Property(e => e.ModelName)
                .HasMaxLength(100)
                .HasColumnName("model_name");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Operational'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.VehicleType)
                .HasMaxLength(50)
                .HasColumnName("vehicle_type");

            entity.HasOne(d => d.AssignedFacility).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.AssignedFacilityId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("vehicles_assigned_facility_id_fkey");

            entity.HasOne(d => d.Island).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.IslandId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("vehicles_island_id_fkey");
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.HasKey(e => e.VisitorId).HasName("visitors_pkey");

            entity.ToTable("visitors", "jurassic_park", t =>
                t.HasCheckConstraint("CK_Visitor_CheckInDate", "check_in_date IS NOT NULL"));

            entity.Property(e => e.VisitorId).HasColumnName("visitor_id");
            entity.Property(e => e.CheckInDate).HasColumnName("check_in_date");
            entity.Property(e => e.CheckOutDate).HasColumnName("check_out_date");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Active'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.TourPackage)
                .HasMaxLength(50)
                .HasColumnName("tour_package");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
