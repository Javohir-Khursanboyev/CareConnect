using AutoMapper;
using CareConnect.Data.UnitOfWorks;
using CareConnect.Domain.Entities.Doctors;
using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.Assets;
using CareConnect.Service.DTOs.Doctors;
using CareConnect.Service.Exceptions;
using CareConnect.Service.Extensions;
using CareConnect.Service.Services.Assets;
using CareConnect.Service.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace CareConnect.Service.Services.Doctors;

public class DoctorService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserService userService,
    IAssetService assetService) : IDoctorService
{
    public async Task<DoctorViewModel> CreateAsync(DoctorCreateModel model)
    {
        await unitOfWork.BeginTransactionAsync();

        model.User.RoleId = await GetRoleId();
        await userService.CreateAsync(model.User);

        var mappedDoctor = mapper.Map<Doctor>(model);
        mappedDoctor.Create();
        var createdDoctor = await unitOfWork.Doctors.InsertAsync(mappedDoctor);

        await unitOfWork.SaveAsync();
        await unitOfWork.CommitTransactionAsync();

        return mapper.Map<DoctorViewModel> (createdDoctor);
    }

    public async Task<DoctorViewModel> UpdateAsync(long id, DoctorUpdateModel model)
    {
        await unitOfWork.BeginTransactionAsync();

        var existDoctor = await unitOfWork.Doctors.SelectAsync(doctor => doctor.Id == id)
            ?? throw new NotFoundException($"Doctor is not found with this ID={id}");

        await userService.UpdateAsync(existDoctor.UserId, model.User);

        var mappedDoctor = mapper.Map(model, existDoctor);
        mappedDoctor.Update();
        await unitOfWork.Doctors.UpdateAsync(mappedDoctor);

        await unitOfWork.SaveAsync();
        await unitOfWork.CommitTransactionAsync();

        return mapper.Map<DoctorViewModel>(existDoctor);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        await unitOfWork.BeginTransactionAsync();

        var existDoctor = await unitOfWork.Doctors.SelectAsync(doctor => doctor.Id == id)
            ?? throw new NotFoundException($"Doctor is not found with this ID={id}");

        await userService.DeleteAsync(existDoctor.UserId);
        existDoctor.Delete();
        await unitOfWork.Doctors.DeleteAsync(existDoctor);

        await unitOfWork.SaveAsync();
        await unitOfWork.CommitTransactionAsync();

        return true;
    }

    public async Task<DoctorViewModel> GetByIdAsync(long id)
    {
        var existDoctor = await unitOfWork.Doctors
            .SelectAsync(doctor => doctor.Id == id && !doctor.IsDeleted, includes: ["User.Role", "Picture"])
            ?? throw new NotFoundException($"Doctor is not found with this ID={id}");

        return mapper.Map<DoctorViewModel>(existDoctor);
    }

    public async Task<IEnumerable<DoctorViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var doctors = unitOfWork.Doctors
           .SelectAsQueryable(expression: doctor => !doctor.IsDeleted, includes: ["User.Role", "Picture"], isTracked: false)
           .OrderBy(filter);

        if(!string.IsNullOrWhiteSpace(search))
            doctors = doctors.Where(doctor =>
                doctor.User.FirstName.ToLower().Contains(search.ToLower()) ||
                doctor.User.LastName.ToLower().Contains(search.ToLower()));

        return mapper.Map<IEnumerable<DoctorViewModel>>(await doctors.ToPaginateAsQueryable(@params).ToListAsync());
    }

    public async Task<DoctorViewModel> UploadPictureAsync(long id, AssetCreateModel assetCreateModel)
    {
        await unitOfWork.BeginTransactionAsync();
        var existDoctor = await unitOfWork.Doctors
            .SelectAsync(doctor => doctor.Id == id && !doctor.IsDeleted, includes: ["User.Role", "Picture"])
            ?? throw new NotFoundException($"Doctor is not found with this ID={id}");

        var createdPicture = await assetService.UploadAsync(assetCreateModel);

        existDoctor.PictureId = createdPicture.Id;
        existDoctor.Update();
        await unitOfWork.Doctors.UpdateAsync(existDoctor);
        await unitOfWork.SaveAsync();

        return mapper.Map<DoctorViewModel>(existDoctor);
    }

    
    public async Task<DoctorViewModel> DeletePictureAsync(long id)
    {
        await unitOfWork.BeginTransactionAsync();
        var existDoctor = await unitOfWork.Doctors
            .SelectAsync(doctor => doctor.Id == id && !doctor.IsDeleted, includes: ["User.Role", "Picture"])
            ?? throw new NotFoundException($"Doctor is not found with this ID={id}");

        await assetService.DeleteAsync(Convert.ToInt64(existDoctor.PictureId));

        existDoctor.PictureId = null;
        existDoctor.Update();
        await unitOfWork.Doctors.UpdateAsync(existDoctor);
        await unitOfWork.SaveAsync();

        return mapper.Map<DoctorViewModel>(existDoctor);
    }

    private async ValueTask<long> GetRoleId()
    {
        var role = await unitOfWork.Roles.SelectAsync(role => role.Name.ToLower() == Constants.DoctorRoleName.ToLower())
            ?? throw new NotFoundException($"Role is not found with this name {Constants.DoctorRoleName}");

        return role.Id;
    }
}
