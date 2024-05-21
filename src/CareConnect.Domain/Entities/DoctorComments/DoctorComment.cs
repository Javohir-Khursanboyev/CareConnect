using CareConnect.Domain.Commons;
using CareConnect.Domain.Entities.Doctors;
using CareConnect.Domain.Entities.Patients;

namespace CareConnect.Domain.Entities.DoctorComments;

public class DoctorComment : Auditable
{
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
    public string Comment { get; set; }
    public long? ParentId { get; set; }

    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }
}
