using CareConnect.Domain.Commons;
using CareConnect.Domain.Entities.Users;
using CareConnect.Domain.Entities.Assets;

namespace CareConnect.Domain.Entities.Patients;

public class Patient : Auditable
{
    public long UserId { get; set; }
    public long? PictureId { get; set; }

    public User User { get; set; }
    public Asset Asset { get; set; }
}
