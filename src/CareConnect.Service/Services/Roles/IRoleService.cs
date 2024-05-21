using CareConnect.Service.DTOs.Roles;
using CareConnect.Service.Configurations;

namespace CareConnect.Service.Services.Roles;

public interface IRoleService
{
    Task<RoleViewModel> CreateAsync(RoleCreateModel model);
    Task<RoleViewModel> UpdateAsync(long id, RoleUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<RoleViewModel> GetByIdAsync(long id);
    Task<IEnumerable<RoleViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
