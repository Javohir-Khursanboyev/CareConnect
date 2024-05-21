using CareConnect.Service.DTOs.Hospitals;

namespace CareConnect.Service.DTOs.Departments;

public class DepartmentViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public HospitalViewModel Hospital { get; set; }
}
