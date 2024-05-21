namespace CareConnect.Domain.Entities.Documents;

public class DocumentsCreateModel
{
    public string Name { get; set; }
    public string Path { get; set; }
    public long PatientId { get; set; }
    public long AppointmentId { get; set; }
}