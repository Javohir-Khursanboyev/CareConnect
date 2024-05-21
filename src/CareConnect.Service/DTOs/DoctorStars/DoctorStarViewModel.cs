using CareConnect.Service.DTOs.Doctors;
using CareConnect.Service.DTOs.Patients;

namespace CareConnect.Service.DTOs.DoctorStars;

public class DoctorStarViewModel
{
    public long Id { get; set; }
    public DoctorViewModel Doctor { get; set; }
    public PatientViewModel Patient { get; set; }
    public byte Star { get; set; }
}
