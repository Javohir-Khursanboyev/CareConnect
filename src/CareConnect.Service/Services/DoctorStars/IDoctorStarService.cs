using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.DoctorStars;

namespace CareConnect.Service.Services.DoctorStars;

public interface IDoctorStarService
{
    Task<DoctorStarViewModel> CreateAsync(DoctorStarCreateModel model);
    Task<bool> DeleteAsync(long id);
    Task<DoctorStarViewModel> GetByIdAsync(long id);
    Task<IEnumerable<DoctorStarViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
