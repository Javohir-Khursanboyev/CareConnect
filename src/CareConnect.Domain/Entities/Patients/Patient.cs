using CareConnect.Domain.Commons;
using CareConnect.Domain.Entities.Users;
using CareConnect.Domain.Entities.Assets;
using CareConnect.Domain.Entities.Documents;
using CareConnect.Domain.Entities.DoctorStars;
using CareConnect.Domain.Entities.Appointments;
using CareConnect.Domain.Entities.DoctorComments;

namespace CareConnect.Domain.Entities.Patients;

public class Patient : Auditable
{
    public long UserId { get; set; }
    public long? PictureId { get; set; }

    public User User { get; set; }
    public Asset Picture { get; set; }

    public IEnumerable<DoctorStar> DoctorStars { get; set; }
    public IEnumerable<Appointment> Appointments { get; set; }
    public IEnumerable<Document> Documents { get; set; }
}