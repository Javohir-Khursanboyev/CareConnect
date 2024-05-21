using CareConnect.Domain.Commons;
using CareConnect.Domain.Entities.Users;
using CareConnect.Domain.Entities.Assets;
using CareConnect.Domain.Entities.Departments;
using CareConnect.Domain.Entities.DoctorStars;
using CareConnect.Domain.Entities.Appointments;
using CareConnect.Domain.Entities.DoctorComments;

namespace CareConnect.Domain.Entities.Doctors;

public class Doctor : Auditable
{
    public string About { get; set; }
    public string Specialty { get; set; }
    public long DepartmentId { get; set; }
    public long? PictureId { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public Asset Picture { get; set; }
    public Department Department { get; set; }

    public IEnumerable<DoctorStar> DoctorStars { get; set; }
    public IEnumerable<Appointment> Appointments { get; set; }
    public IEnumerable<DoctorComment> DoctorComments { get; set; }
}
