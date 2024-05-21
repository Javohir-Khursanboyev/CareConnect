using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.Permissions;

namespace CareConnect.Service.Services.Permissions;

public interface IPermissionService
{
    Task<PermissionViewModel> CreateAsync(PermissionCreateModel model);
    Task<PermissionViewModel> UpdateAsync(long id, PermissionUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<PermissionViewModel> GetByIdAsync(long id);
    Task<IEnumerable<PermissionViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
