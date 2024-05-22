using CareConnect.Domain.Entities.DoctorComments;
using Microsoft.EntityFrameworkCore;

namespace Arcana.DataAccess.EntityConfigurations.Commons;

public class DoctorCommentConfiguration : IEntityConfiguration
{
    public void Configure(ModelBuilder modelBuilder)
    {
        // DoctorComment and Patient
        modelBuilder.Entity<DoctorComment>()
            .HasOne(doctorComment => doctorComment.Patient)
            .WithMany()
            .HasForeignKey(doctorComment => doctorComment.PatientId)
            .OnDelete(DeleteBehavior.NoAction);

        // DoctorComment and Doctor
        modelBuilder.Entity<DoctorComment>()
            .HasOne(doctorComment => doctorComment.Doctor)
            .WithMany(doctor => doctor.DoctorComments)
            .HasForeignKey(doctorComment => doctorComment.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DoctorComment>().HasData(
               new DoctorComment { Id = 1, DoctorId = 1, PatientId = 1, Comment = "Great doctor!", CreatedAt = DateTime.UtcNow }
           );
    }
}