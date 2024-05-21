namespace CareConnect.Service.DTOs.Doctors;

public class DoctorCreateModel
{
    public string Specialty { get; set; }
    public long DepartmentId { get; set; }
    public long? PictureId { get; set; }
    public long ResumeId { get; set; }
}
