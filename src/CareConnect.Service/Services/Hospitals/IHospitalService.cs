using CareConnect.Service.DTOs.Assets;
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
    Task<HospitalViewModel> UploadPictureAsync(long id, AssetCreateModel assetCreateModel);
    Task<HospitalViewModel> DeletePictureAsync(long id);
}
