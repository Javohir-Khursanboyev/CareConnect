namespace CareConnect.Service.DTOs.DoctorComments;

public class DoctorCommentCreateModel
{
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
    public string Comment { get; set; }
    public long? ParentId { get; set; }
}
