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

    }
}