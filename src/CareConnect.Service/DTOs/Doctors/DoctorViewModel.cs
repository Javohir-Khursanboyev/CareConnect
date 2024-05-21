using CareConnect.Service.DTOs.Assets;
using CareConnect.Service.DTOs.Departments;
using CareConnect.Service.DTOs.Users;

namespace CareConnect.Service.DTOs.Doctors;

public class DoctorViewModel
{
    public long Id { get; set; }
    public string Specialty { get; set; }
    public string About { get; set; }
    public UserViewModel User { get; set; }
    public DepartmentViewModel Department { get; set; }
    public AssetViewModel Picture { get; set; }
}