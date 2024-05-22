using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.Departments;

namespace CareConnect.Service.Services.Departments;

public interface IDepartmentService
{
    Task<DepartmentViewModel> CreateAsync(DepartmentCreateModel model);
    Task<DepartmentViewModel> UpdateAsync(long id, DepartmentUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<DepartmentViewModel> GetByIdAsync(long id);
    Task<IEnumerable<DepartmentViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
