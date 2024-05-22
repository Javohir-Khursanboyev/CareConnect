using CareConnect.Domain.Entities.Doctors;
using Microsoft.EntityFrameworkCore;

namespace Arcana.DataAccess.EntityConfigurations.Commons;

public class DoctorConfiguration : IEntityConfiguration
{
    public void Configure(ModelBuilder modelBuilder)
    {
        // Doctor and User
        modelBuilder.Entity<Doctor>()
            .HasOne(doctor => doctor.User)
            .WithMany()
            .HasForeignKey(doctor => doctor.UserId);

        // Doctor and Asset
        modelBuilder.Entity<Doctor>()
            .HasOne(doctor => doctor.Picture)
            .WithMany()
            .HasForeignKey(doctor => doctor.PictureId);

        // Doctor and Department
        modelBuilder.Entity<Doctor>()
            .HasOne(doctor => doctor.Department)
            .WithMany(deparment => deparment.Doctors)
            .HasForeignKey(doctor => doctor.DepartmentId);
    }

    public void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id =1,
                    About = "Experienced Cardiologist",
                    Specialty = "Cardiology",
                    DepartmentId = 1,
                    PictureId = 1,
                    UserId = 3,
                    CreatedAt = DateTime.UtcNow
                });
    }
}