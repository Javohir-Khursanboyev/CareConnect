using AutoMapper;
using CareConnect.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using CareConnect.Service.Extensions;
using CareConnect.Service.Exceptions;
using CareConnect.Service.DTOs.Assets;
using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.Hospitals;
using CareConnect.Service.Services.Assets;
using CareConnect.Domain.Entities.Hospitals;

namespace CareConnect.Service.Services.Hospitals;

public class HospitalService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IAssetService assetService) : IHospitalService
{
    public async Task<HospitalViewModel> CreateAsync(HospitalCreateModel model)
    {
        await unitOfWork.BeginTransactionAsync();

        var existHospital = await unitOfWork.Hospitals.
            SelectAsync(h => h.Name.ToLower() == model.Name.ToLower() && h.Address.ToLower() == model.Address.ToLower());

        if (existHospital is not null)
            throw new AlreadyExistException("Hospital is already exist");

        var hospital = mapper.Map<Hospital>(model);
        hospital.Create();

        var createdHospital = await unitOfWork.Hospitals.InsertAsync(hospital);
        await unitOfWork.SaveAsync();
        await unitOfWork.CommitTransactionAsync();

        return mapper.Map<HospitalViewModel>(createdHospital);
    }

    public async Task<HospitalViewModel> UpdateAsync(long id, HospitalUpdateModel model)
    {
        await unitOfWork.BeginTransactionAsync();

        var existHospital = await unitOfWork.Hospitals.SelectAsync(h => h.Id == id)
            ?? throw new NotFoundException("Hospital is not found");

        var alreadyExistHospital = await unitOfWork.Hospitals.
              SelectAsync(h => h.Name.ToLower() == model.Name.ToLower() && h.Address.ToLower() == model.Address.ToLower());

        if (alreadyExistHospital is not null)
            throw new AlreadyExistException("Hospital is already exist");

        mapper.Map(model, existHospital);
        existHospital.Update();
        var updateHospital = await unitOfWork.Hospitals.UpdateAsync(existHospital);
        await unitOfWork.SaveAsync();
        await unitOfWork.CommitTransactionAsync();

        return mapper.Map<HospitalViewModel>(updateHospital);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        await unitOfWork.BeginTransactionAsync();

        var existHospital = await unitOfWork.Hospitals.SelectAsync(h => h.Id == id)
            ?? throw new NotFoundException("Hospital is not found");

        await unitOfWork.Hospitals.DropAsync(existHospital);
        await unitOfWork.SaveAsync();
        await unitOfWork.CommitTransactionAsync();

        return true;
    }

    public async Task<HospitalViewModel> GetByIdAsync(long id)
    {
        var existHospital = await unitOfWork.Hospitals.
            SelectAsync(expression: h => h.Id == id, includes: ["Asset"])
            ?? throw new NotFoundException("Hospital is not found");

        return mapper.Map<HospitalViewModel>(existHospital);
    }

    public async Task<IEnumerable<HospitalViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var hospitals = unitOfWork.Hospitals.
            SelectAsQueryable(includes: ["Asset"], isTracked: false).
            OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            hospitals = hospitals.Where(h =>
            h.Name.ToLower().Contains(search.ToLower()) ||
            h.Address.ToLower().Contains(search.ToLower()));

        var paginateHospitals = await hospitals.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<HospitalViewModel>>(paginateHospitals);
    }

    public async Task<HospitalViewModel> UploadPictureAsync(long id, AssetCreateModel assetCreateModel)
    {
        await unitOfWork.BeginTransactionAsync();
        var existHospital = await unitOfWork.Hospitals
            .SelectAsync(h => h.Id == id && !h.IsDeleted, includes: ["Asset"])
            ?? throw new NotFoundException($"Hospital is not found with this ID={id}");

        var createdPicture = await assetService.UploadAsync(assetCreateModel);

        existHospital.AssetId = createdPicture.Id;
        existHospital.Update();
        await unitOfWork.Hospitals.UpdateAsync(existHospital);
        await unitOfWork.SaveAsync();

        return mapper.Map<HospitalViewModel>(existHospital);
    }


    public async Task<HospitalViewModel> DeletePictureAsync(long id)
    {
        await unitOfWork.BeginTransactionAsync();
        var existHospital = await unitOfWork.Hospitals
            .SelectAsync(h => h.Id == id && !h.IsDeleted, includes: ["Asset"])
            ?? throw new NotFoundException($"Hospital is not found with this ID={id}");

        await assetService.DeleteAsync(Convert.ToInt64(existHospital.AssetId));

        existHospital.AssetId = null;
        existHospital.Update(); 
        await unitOfWork.Hospitals.UpdateAsync(existHospital);
        await unitOfWork.SaveAsync();

        return mapper.Map<HospitalViewModel>(existHospital);
    }
}
