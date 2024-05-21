using CareConnect.Domain.Commons;
using CareConnect.Domain.Entities.Appointments;

namespace CareConnect.Domain.Entities.Recommendations;

public class Recommendation : Auditable
{
    public long AppointmentId { get; set; }
    public string Prescription { get; set; }

    public Appointment Appointment { get; set; }
}
