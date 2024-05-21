using CareConnect.Service.DTOs.Assets;
using CareConnect.Service.DTOs.Departments;

namespace CareConnect.Service.DTOs.Doctors;

public class DoctorViewModel
{
    public long Id { get; set; }
    public string Specialty { get; set; }
    public DepartmentViewModel Department { get; set; }
    public AssetViewModel Picture { get; set; }
    public AssetViewModel Resume { get; set; }
}