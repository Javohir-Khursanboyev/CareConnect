using CareConnect.Domain.Entities.Departments;
using Microsoft.EntityFrameworkCore;

namespace Arcana.DataAccess.EntityConfigurations.Commons;

public class DepartmentConfiguration : IEntityConfiguration
{
    public void Configure(ModelBuilder modelBuilder)
    {
        // Department and Hospital
        modelBuilder.Entity<Department>()
            .HasOne(department => department.Hospital)
            .WithMany(hospital => hospital.Departments)
            .HasForeignKey(department => department.HospitalId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>().HasData(
           new Department { Id = 1, Name = "Cardiology", HospitalId = 1, CreatedAt = DateTime.UtcNow},
           new Department { Id = 2, Name = "Neurology", HospitalId = 1, CreatedAt = DateTime.UtcNow },
           new Department { Id = 3, Name = "Pediatrics", HospitalId = 1, CreatedAt = DateTime.UtcNow }
       );
    }
}