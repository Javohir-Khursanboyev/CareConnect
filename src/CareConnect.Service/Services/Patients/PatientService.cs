using AutoMapper;
using CareConnect.Data.UnitOfWorks;
using CareConnect.Domain.Entities.Patients;
using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.Assets;
using CareConnect.Service.DTOs.Patients;
using CareConnect.Service.Exceptions;
using CareConnect.Service.Extensions;
using CareConnect.Service.Services.Assets;
using CareConnect.Service.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace CareConnect.Service.Services.Patients;

public class PatientService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IAssetService assetService,
    IUserService userService) : IPatientService
{
    public async Task<PatientViewModel> CreateAsync(PatientCreateModel model)
    {
        await unitOfWork.BeginTransactionAsync();

        model.User.RoleId = await GetRoleId();
        await userService.CreateAsync(model.User);

        var mappedPatinet = mapper.Map<Patient>(model);
        mappedPatinet.Create();
        var createdDoctor = await unitOfWork.Patients.InsertAsync(mappedPatinet);

        await unitOfWork.SaveAsync();
        await unitOfWork.CommitTransactionAsync();

        return mapper.Map<PatientViewModel>(createdDoctor);
    }

    public async Task<PatientViewModel> UpdateAsync(long id, PatientUpdateModel model)
    {
        await unitOfWork.BeginTransactionAsync();

        var existPatient = await unitOfWork.Patients.SelectAsync(patient => patient.Id == id && !patient.IsDeleted)
            ?? throw new NotFoundException($"Patient is not found with this ID={id}");

        await userService.UpdateAsync(existPatient.UserId, model.User);

        var mappedPatient = mapper.Map(model, existPatient);
        mappedPatient.Update();
        await unitOfWork.Patients.UpdateAsync(mappedPatient);
        await unitOfWork.SaveAsync();
        await unitOfWork.CommitTransactionAsync();

        return mapper.Map<PatientViewModel>(existPatient);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        await unitOfWork.BeginTransactionAsync();

        var existPatient = await unitOfWork.Patients.SelectAsync(patient => patient.Id == id && !patient.IsDeleted)
            ?? throw new NotFoundException($"Patient is not found with this ID={id}");

        await userService.DeleteAsync(existPatient.UserId);
        existPatient.Delete();
        await unitOfWork.Patients.DeleteAsync(existPatient);

        await unitOfWork.SaveAsync();
        await unitOfWork.CommitTransactionAsync();

        return true;
    }

    public async Task<PatientViewModel> GetByIdAsync(long id)
    {
        var existPatient = await unitOfWork.Patients
            .SelectAsync(patient => patient.Id == id && !patient.IsDeleted, includes: ["User.Role", "Picture"])
            ?? throw new NotFoundException($"Patient is not found with this ID={id}");

        return mapper.Map<PatientViewModel>(existPatient);
    }

    public async Task<IEnumerable<PatientViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var patients = unitOfWork.Patients
           .SelectAsQueryable(expression: patient => !patient.IsDeleted, includes: ["User.Role", "Picture"], isTracked: false)
           .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            patients = patients.Where(patient =>
                patient.User.FirstName.ToLower().Contains(search.ToLower()) ||
                patient.User.LastName.ToLower().Contains(search.ToLower()));

        return mapper.Map<IEnumerable<PatientViewModel>>(await patients.ToPaginateAsQueryable(@params).ToListAsync());
    }

    public async Task<PatientViewModel> UploadPictureAsync(long id, AssetCreateModel assetCreateModel)
    {
        await unitOfWork.BeginTransactionAsync();
        var existPatient = await unitOfWork.Patients
            .SelectAsync(patient => patient.Id == id && !patient.IsDeleted, includes: ["User.Role", "Picture"])
            ?? throw new NotFoundException($"Patient is not found with this ID={id}");

        var createdPicture = await assetService.UploadAsync(assetCreateModel);

        existPatient.PictureId = createdPicture.Id;
        existPatient.Update();
        await unitOfWork.Patients.UpdateAsync(existPatient);
        await unitOfWork.SaveAsync();

        return mapper.Map<PatientViewModel>(existPatient);
    }

    public async Task<PatientViewModel> DeletePictureAsync(long id)
    {
        await unitOfWork.BeginTransactionAsync();
        var existPatient = await unitOfWork.Patients
            .SelectAsync(patient => patient.Id == id && !patient.IsDeleted, includes: ["User.Role", "Picture"])
            ?? throw new NotFoundException($"Patient is not found with this ID={id}");

        await assetService.DeleteAsync(Convert.ToInt64(existPatient.PictureId));

        existPatient.PictureId = null;
        existPatient.Update();
        await unitOfWork.Patients.UpdateAsync(existPatient);
        await unitOfWork.SaveAsync();

        return mapper.Map<PatientViewModel>(existPatient);
    }

    private async ValueTask<long> GetRoleId()
    {
        var role = await unitOfWork.Roles.SelectAsync(role => role.Name.ToLower() == Constants.PatientRoleName.ToLower())
            ?? throw new NotFoundException($"Role is not found with this name {Constants.PatientRoleName}");

        return role.Id;
    }
}