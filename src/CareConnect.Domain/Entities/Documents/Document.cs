using CareConnect.Domain.Commons;
using CareConnect.Domain.Entities.Patients;
using CareConnect.Domain.Entities.Appointments;

namespace CareConnect.Domain.Entities.Documents;

public class Document : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
    public long PatientId { get; set; }
    public long AppointmentId { get; set; }

    public Patient Patient { get; set; }
    public Appointment Appointment { get; set; }
}