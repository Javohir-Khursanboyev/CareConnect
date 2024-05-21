using CareConnect.Data.DbContexts;
using CareConnect.Data.Repositories;
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
using Microsoft.EntityFrameworkCore.Storage;

namespace CareConnect.Data.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IRepository<User> Users {  get; }
    public IRepository<Role> Roles { get; }
    public IRepository<Asset> Assets { get; }
    public IRepository<Doctor> Doctors { get; }
    public IRepository<Patient> Patients { get; }
    public IRepository<Document> Documents { get; }
    public IRepository<Hospital> Hospitals { get; }
    public IRepository<DoctorStar> DoctorStars { get; }
    public IRepository<Department> Departments { get; }
    public IRepository<Permission> Permissions { get; }
    public IRepository<Appointment> Appointments { get; }
    public IRepository<DoctorComment> DoctorComments { get; }
    public IRepository<Recommendation> Recommendations { get; }
    public IRepository<RolePermission> RolePermissions { get; }
    private IDbContextTransaction transaction;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Roles = new Repository<Role>(_context);
        Users = new Repository<User>(_context);
        Assets = new Repository<Asset>(_context);
        Doctors = new Repository<Doctor>(_context);
        Patients = new Repository<Patient>(_context);
        Documents = new Repository<Document>(_context);
        Hospitals = new Repository<Hospital>(_context);
        DoctorStars = new Repository<DoctorStar>(_context);
        Departments = new Repository<Department>(_context);
        Permissions = new Repository<Permission>(_context);
        Appointments = new Repository<Appointment>(_context);
        DoctorComments = new Repository<DoctorComment>(_context);
        Recommendations = new Repository<Recommendation>(_context);
        RolePermissions = new Repository<RolePermission>(_context);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async ValueTask<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async ValueTask BeginTransactionAsync()
    {
        transaction = await _context.Database.BeginTransactionAsync();
    }

    public async ValueTask CommitTransactionAsync()
    {
        await transaction.CommitAsync();
    }
}
