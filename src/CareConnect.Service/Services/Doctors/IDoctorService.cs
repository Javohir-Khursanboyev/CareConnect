using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.Assets;
using CareConnect.Service.DTOs.Doctors;
using Microsoft.AspNetCore.Http;

namespace CareConnect.Service.Services.Doctors;

public interface IDoctorService
{
    Task<DoctorViewModel> CreateAsync(DoctorCreateModel model);
    Task<DoctorViewModel> UpdateAsync(long id, DoctorUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<DoctorViewModel> GetByIdAsync(long id);
    Task<IEnumerable<DoctorViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<DoctorViewModel> UploadPictureAsync(long id, AssetCreateModel assetCreateModel);
    Task<DoctorViewModel> DeletePictureAsync(long id);
}