using CareConnect.Domain.Entities.Appointments;
using CareConnect.Service.DTOs.Appointments;
using CareConnect.Service.DTOs.Patients;

namespace CareConnect.Domain.Entities.Documents;

public class DocumentViewModel
{
    public string Name { get; set; }
    public string Path { get; set; }
    public PatientViewModel Patient { get; set; }
    public AppointmentViewModel Appointment { get; set; }
}