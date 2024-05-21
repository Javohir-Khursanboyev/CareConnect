using CareConnect.Domain.Commons;

namespace CareConnect.Domain.Entities.Users;

public class Role : Auditable
{
    public string Name { get; set; }

    public IEnumerable<User> Users { get; set; }
}