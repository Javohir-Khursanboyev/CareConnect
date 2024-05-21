using CareConnect.Service.DTOs.Appointments;

namespace CareConnect.Domain.Entities.Recommendations;

public class RecommendationViewModel
{
    public AppointmentViewModel Appointment { get; set; }
    public string Prescription { get; set; }
}