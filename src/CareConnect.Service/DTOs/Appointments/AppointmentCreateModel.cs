using CareConnect.Domain.Enums;

namespace CareConnect.Service.DTOs.Appointments;

public class AppointmentCreateModel
{
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
    public DateTime Date { get; set; }
    public int Duration { get; set; }
    public AppointmentStatus Status { get; set; }
}
