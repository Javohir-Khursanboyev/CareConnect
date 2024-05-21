using CareConnect.Domain.Enums;
using CareConnect.Service.DTOs.Doctors;
using CareConnect.Service.DTOs.Patients;

namespace CareConnect.Service.DTOs.Appointments;

public class AppointmentViewModel
{
    public long Id { get; set; }
    public DoctorViewModel Doctor { get; set; }
    public PatientViewModel Patient { get; set; }
    public DateTime Date { get; set; }
    public int Duration { get; set; }
    public AppointmentStatus Status { get; set; }
}