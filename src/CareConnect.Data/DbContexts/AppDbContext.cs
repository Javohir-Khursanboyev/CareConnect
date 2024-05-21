using Microsoft.EntityFrameworkCore;
using CareConnect.Domain.Entities.Users;
using CareConnect.Domain.Entities.Assets;
using CareConnect.Domain.Entities.Doctors;
using CareConnect.Domain.Entities.Patients;
using CareConnect.Domain.Entities.Documents;
using CareConnect.Domain.Entities.Hospitals;
using CareConnect.Domain.Entities.DoctorStars;
using CareConnect.Domain.Entities.Departments;
using CareConnect.Domain.Entities.Appointments;
using CareConnect.Domain.Entities.DoctorComments;
using CareConnect.Domain.Entities.Recommendations;

namespace CareConnect.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Hospital> Hospitals { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<DoctorStar> DoctorStars { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<DoctorComment> DoctorComments { get; set; }
    public DbSet<Recommendation> Recommendations { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
}
