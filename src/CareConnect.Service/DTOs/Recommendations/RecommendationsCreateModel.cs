namespace CareConnect.Domain.Entities.Recommendations;

public class RecommendationsCreateModel
{
    public long AppointmentId { get; set; }
    public string Prescription { get; set; }
}