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

    }
}