using CareConnect.Domain.Commons;

namespace CareConnect.Domain.Entities.Doctors;

public class Doctor : Auditable
{
    public string Specialty { get; set; }

}
