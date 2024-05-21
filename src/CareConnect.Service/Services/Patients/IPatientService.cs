using AutoMapper;
using CareConnect.Data.UnitOfWorks;
using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.Assets;
using CareConnect.Service.DTOs.Patients;
using CareConnect.Service.Services.Assets;
using CareConnect.Service.Services.Users;

namespace CareConnect.Service.Services.Patients;

public interface IPatientService
{
    Task<PatientViewModel> CreateAsync(PatientCreateModel model);
    Task<PatientViewModel> UpdateAsync(long id, PatientUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<PatientViewModel> GetByIdAsync(long id);
    Task<IEnumerable<PatientViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<PatientViewModel> UploadPictureAsync(long id, AssetCreateModel assetCreateModel);
    Task<PatientViewModel> DeletePictureAsync(long id);
}