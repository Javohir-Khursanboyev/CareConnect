using CareConnect.Domain.Entities.Hospitals;
using CareConnect.Domain.Entities.Recommendations;
using Microsoft.EntityFrameworkCore;

namespace Arcana.DataAccess.EntityConfigurations.Commons;

public class RecommendationConfiguration : IEntityConfiguration
{
    public void Configure(ModelBuilder modelBuilder)
    {
        // Recommendation and Appointment
        modelBuilder.Entity<Recommendation>()
            .HasOne(recommendation => recommendation.Appointment)
            .WithMany()
            .HasForeignKey(recommendation => recommendation.AppointmentId);
    }

    public void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recommendation>().HasData(
                new Recommendation
                {
                    Id = 1,
                    AppointmentId = 1,
                    Prescription = "Recommendation1",
                    CreatedAt = DateTime.UtcNow
                });
    }
}