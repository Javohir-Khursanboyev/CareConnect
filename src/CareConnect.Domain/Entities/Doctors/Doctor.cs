using CareConnect.Domain.Commons;
using CareConnect.Domain.Entities.Users;
using CareConnect.Domain.Entities.Assets;
using CareConnect.Domain.Entities.Departments;

namespace CareConnect.Domain.Entities.Doctors;

public class Doctor : Auditable
{
    public string Specialty { get; set; }
    public long DepartmentId { get; set; }
    public long? PictureId { get; set; }
    public long ResumeId { get; set; }

    public User User { get; set; }
    public Asset Asset { get; set; }
    public Department Department { get; set; }
}
