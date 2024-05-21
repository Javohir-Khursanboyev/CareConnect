using CareConnect.Service.DTOs.Users;

namespace CareConnect.Service.DTOs.Doctors;

public class DoctorUpdateModel
{
    public UserUpdateModel User {  get; set; }
    public string Specialty { get; set; }
    public long DepartmentId { get; set; }
    public string About { get; set; }
}
