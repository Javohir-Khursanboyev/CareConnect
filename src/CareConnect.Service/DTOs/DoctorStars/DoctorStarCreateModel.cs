namespace CareConnect.Service.DTOs.DoctorStars;

public class DoctorStarCreateModel
{
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
    public byte Star { get; set; }
}
