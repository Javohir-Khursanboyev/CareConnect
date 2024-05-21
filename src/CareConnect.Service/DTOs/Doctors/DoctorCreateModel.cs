using CareConnect.Service.DTOs.Users;

namespace CareConnect.Service.DTOs.Doctors;

public class DoctorCreateModel
{
    public UserCreateModel User { get; set; }
    public string Specialty { get; set; }
    public string About { get; set; }
    public long DepartmentId { get; set; }
}