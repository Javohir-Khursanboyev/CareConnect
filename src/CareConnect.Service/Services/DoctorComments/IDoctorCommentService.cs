using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.DoctorComments;

namespace CareConnect.Service.Services.DoctorComments;

public interface IDoctorCommentService
{
    Task<DoctorCommentViewModel> CreateAsync(DoctorCommentCreateModel model);
    Task<DoctorCommentViewModel> UpdateAsync(long id, DoctorCommentUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<DoctorCommentViewModel> GetByIdAsync(long id);
    Task<IEnumerable<DoctorCommentViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}