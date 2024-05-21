using CareConnect.Domain.Commons;

namespace CareConnect.Domain.Entities.Users;

public class RolePermission : Auditable
{
    public long RoleId { get; set; }
    public long PermissionId { get; set; }

    public Role Role { get; set; }
    public Permission Permission { get; set; }
}