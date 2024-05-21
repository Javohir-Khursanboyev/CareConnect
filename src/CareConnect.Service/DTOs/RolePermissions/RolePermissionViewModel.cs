using CareConnect.Service.DTOs.Roles;
using CareConnect.Service.DTOs.Permissions;

namespace CareConnect.Service.DTOs.RolePermissions;

public class RolePermissionViewModel
{
    public long Id { get; set; }
    public RoleViewModel Role { get; set; }
    public PermissionViewModel Permission { get; set; }
}
