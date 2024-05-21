using CareConnect.Domain.Commons;
using CareConnect.Domain.Entities.Doctors;
using CareConnect.Domain.Entities.Patients;

namespace CareConnect.Domain.Entities.DoctorStars;

public class DoctorStar : Auditable
{
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
    public byte Star { get; set; }

    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }
}
