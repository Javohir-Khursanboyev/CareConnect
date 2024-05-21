using CareConnect.Service.DTOs.Doctors;
using CareConnect.Service.DTOs.Patients;

namespace CareConnect.Service.DTOs.DoctorComments;

public class DoctorCommentViewModel
{
    public long Id { get; set; }
    public DoctorViewModel Doctor { get; set; }
    public PatientViewModel Patient { get; set; }
    public string Comment { get; set; }
    public DoctorCommentViewModel ParentComment { get; set; }
}
