using CareConnect.Domain.Enums;
using CareConnect.Domain.Commons;
using CareConnect.Domain.Entities.Doctors;
using CareConnect.Domain.Entities.Patients;
using CareConnect.Domain.Entities.Recommendations;

namespace CareConnect.Domain.Entities.Appointments;

public class Appointment : Auditable
{
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
    public DateTime Date { get; set; }
    public int Duration { get; set; }
    public AppointmentStatus Status { get; set; }

    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }

    public IEnumerable<Recommendation> Recommendations { get; set; }
}
