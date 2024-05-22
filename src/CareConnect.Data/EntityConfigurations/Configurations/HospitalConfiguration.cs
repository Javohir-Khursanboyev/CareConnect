using CareConnect.Domain.Entities.Hospitals;
using Microsoft.EntityFrameworkCore;

namespace Arcana.DataAccess.EntityConfigurations.Commons;

public class HospitalConfiguration : IEntityConfiguration
{
    public void Configure(ModelBuilder modelBuilder)
    {
        // Hospital and Asset
        modelBuilder.Entity<Hospital>()
            .HasOne(patient => patient.Asset)
            .WithMany()
            .HasForeignKey(patient => patient.AssetId);
    }

    public void SeedData(ModelBuilder modelBuilder)
    {
        // Hospital
        modelBuilder.Entity<Hospital>().HasData(
             new Hospital
             {
                 Id = 1,
                 Name = "Central Hospital",
                 Address = "123 Main St",
                 Phone = "+998975551234",
                 Email = "info@centralhospital.com",
                 Description = "A large central hospital.",
                 AssetId = 1,
                 CreatedAt = DateTime.UtcNow
             });
    }
}