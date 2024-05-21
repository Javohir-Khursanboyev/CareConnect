namespace CareConnect.Domain.Entities.Recommendations;

public class RecommendationCreateModel
{
    public long AppointmentId { get; set; }
    public string Prescription { get; set; }
}