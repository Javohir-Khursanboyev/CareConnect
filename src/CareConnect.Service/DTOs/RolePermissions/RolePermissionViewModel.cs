using CareConnect.Service.DTOs.Roles;
using CareConnect.Service.DTOs.Permissions;

namespace CareConnect.Service.DTOs.RolePermissions;

public class RolePermissionViewModel
{
    public long Id { get; set; }
    public RoleViewModel RoleId { get; set; }
    public PermissionViewModel PermissionId { get; set; }
}
