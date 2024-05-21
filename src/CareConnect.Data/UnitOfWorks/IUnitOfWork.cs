using CareConnect.Data.Repositories;
using CareConnect.Domain.Entities.Users;
using CareConnect.Domain.Entities.Assets;
using CareConnect.Domain.Entities.Doctors;
using CareConnect.Domain.Entities.Patients;
using CareConnect.Domain.Entities.Hospitals;
using CareConnect.Domain.Entities.Documents;
using CareConnect.Domain.Entities.Departments;
using CareConnect.Domain.Entities.DoctorStars;
using CareConnect.Domain.Entities.Appointments;
using CareConnect.Domain.Entities.DoctorComments;
using CareConnect.Domain.Entities.Recommendations;

namespace CareConnect.Data.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Role> Roles { get; }
    IRepository<Asset> Assets { get; }
    IRepository<Doctor> Doctors { get; }
    IRepository<Patient> Patients { get; }
    IRepository<Document> Documents { get; }
    IRepository<Hospital> Hospitals { get; }
    IRepository<DoctorStar> DoctorStars { get; }
    IRepository<Department> Departments { get; }
    IRepository<Permission> Permissions { get; }
    IRepository<Appointment> Appointments {  get; }
    IRepository<DoctorComment> DoctorComments { get; }
    IRepository<Recommendation> Recommendations { get; }
    IRepository<RolePermission> RolePermissions { get; }

    ValueTask<bool> SaveAsync();
}
