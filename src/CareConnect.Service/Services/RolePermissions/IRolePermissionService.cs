using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.RolePermissions;

namespace CareConnect.Service.Services.RolePermissions;

public interface IRolePermissionService
{
    Task<RolePermissionViewModel> CreateAsync(RolePermissionCreateModel model);
    Task<RolePermissionViewModel> UpdateAsync(long id, RolePermissionUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<RolePermissionViewModel> GetByIdAsync(long id);
    Task<IEnumerable<RolePermissionViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    bool CheckRolePermission(string role, string action, string controller);
}
