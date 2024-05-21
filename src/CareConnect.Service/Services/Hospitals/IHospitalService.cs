using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.Hospitals;

namespace CareConnect.Service.Services.Hospitals;

public interface IHospitalService
{
    Task<HospitalViewModel> CreateAsync(HospitalCreateModel model);
    Task<HospitalViewModel> UpdateAsync(long id, HospitalUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<HospitalViewModel> GetByIdAsync(long id);
    Task<IEnumerable<HospitalViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
