using CareConnect.Domain.Commons;
using CareConnect.Domain.Entities.Doctors;
using CareConnect.Domain.Entities.Hospitals;

namespace CareConnect.Domain.Entities.Departments;

public class Department : Auditable
{
    public string Name { get; set; }
    public long HospitalId { get; set; }

    public Hospital Hospital { get; set; }

    public IEnumerable<Doctor> Doctors { get; set; }
}
