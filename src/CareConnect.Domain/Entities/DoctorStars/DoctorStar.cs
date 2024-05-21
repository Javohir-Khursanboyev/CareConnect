using CareConnect.Domain.Commons;

namespace CareConnect.Domain.Entities.DoctorStars;

public class DoctorStar : Auditable
{
    public long DoctorId { get; set; }
    public long PatientId { get; set; }


}
