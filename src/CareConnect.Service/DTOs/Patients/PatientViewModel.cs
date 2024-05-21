using CareConnect.Service.DTOs.Assets;
using CareConnect.Service.DTOs.Users;

namespace CareConnect.Service.DTOs.Patients;

public class PatientViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    public AssetViewModel Picture { get; set; }
}