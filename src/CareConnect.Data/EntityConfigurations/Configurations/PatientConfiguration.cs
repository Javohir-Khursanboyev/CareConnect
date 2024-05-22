using CareConnect.Domain.Entities.Patients;
using Microsoft.EntityFrameworkCore;

namespace Arcana.DataAccess.EntityConfigurations.Commons;

public class PatientConfiguration : IEntityConfiguration
{
    public void Configure(ModelBuilder modelBuilder)
    {
        // Patient and User
        modelBuilder.Entity<Patient>()
            .HasOne(patient => patient.User)
            .WithMany()
            .HasForeignKey(patient => patient.UserId);

        // Patient and Asset
        modelBuilder.Entity<Patient>()
            .HasOne(patient => patient.Picture)
            .WithMany()
            .HasForeignKey(patient => patient.PictureId);
    }

    public void SeedData(ModelBuilder modelBuilder)
    {

    }
}