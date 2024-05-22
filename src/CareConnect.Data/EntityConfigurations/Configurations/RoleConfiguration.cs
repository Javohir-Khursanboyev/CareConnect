using Arcana.DataAccess.EntityConfigurations.Commons;
using CareConnect.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace CareConnect.Data.EntityConfigurations.Configurations;

public class RoleConfiguration : IEntityConfiguration
{
    public void Configure(ModelBuilder modelBuilder)
    {
        
    }

    public void SeedData(ModelBuilder modelBuilder)
    {
        // Role
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin", CreatedAt = DateTime.UtcNow },
            new Role { Id = 2, Name = "Patient", CreatedAt = DateTime.UtcNow },
            new Role { Id = 3, Name = "Doctor", CreatedAt = DateTime.UtcNow });
    }
}
