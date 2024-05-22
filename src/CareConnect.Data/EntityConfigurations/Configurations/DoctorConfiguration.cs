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
    }

    public void SeedData(ModelBuilder modelBuilder)
    {

    }
}