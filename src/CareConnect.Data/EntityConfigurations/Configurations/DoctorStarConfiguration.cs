using CareConnect.Domain.Entities.DoctorStars;
using Microsoft.EntityFrameworkCore;

namespace Arcana.DataAccess.EntityConfigurations.Commons;

public class DoctorStarConfiguration : IEntityConfiguration
{
    public void Configure(ModelBuilder modelBuilder)
    {
        // DoctorStar and Student
        modelBuilder.Entity<DoctorStar>()
            .HasOne(doctorStar => doctorStar.Patient)
            .WithMany()
            .HasForeignKey(doctorStar => doctorStar.PatientId)
            .OnDelete(DeleteBehavior.NoAction);

        // DoctorStar and Doctor
        modelBuilder.Entity<DoctorStar>()
            .HasOne(doctorStar => doctorStar.Doctor)
            .WithMany(doctor => doctor.DoctorStars)
            .HasForeignKey(doctorStar => doctorStar.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public void SeedData(ModelBuilder modelBuilder)
    {

    }
}