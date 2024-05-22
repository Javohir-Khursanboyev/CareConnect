using AutoMapper;
using CareConnect.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using CareConnect.Service.Exceptions;
using CareConnect.Service.Extensions;
using CareConnect.Domain.Entities.Users;
using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.Permissions;
using CareConnect.Service.Validators.Permissions;

namespace CareConnect.Service.Services.Permissions;

public class PermissionService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    PermissionCreateModelValidator createModelValidator,
    PermissionUpdateModelValidator updateModelValidator) : IPermissionService
{
    public async Task<PermissionViewModel> CreateAsync(PermissionCreateModel model)
    {
        await createModelValidator.EnsureValidatedAsync(model);
        var existPermission = await unitOfWork.Permissions.
            SelectAsync(p => p.Action.ToLower() == model.Action.ToLower() && p.Controller.ToLower() == model.Controller.ToLower());

        if (existPermission is not null)
            throw new AlreadyExistException("Permission is already exist");

        var permission = mapper.Map<Permission>(model);
        permission.Create();
        var createdPermission = await unitOfWork.Permissions.InsertAsync(permission);
        await unitOfWork.SaveAsync();

        return mapper.Map<PermissionViewModel>(createdPermission);
    }

    public async Task<PermissionViewModel> UpdateAsync(long id, PermissionUpdateModel model)
    {
        await updateModelValidator.EnsureValidatedAsync(model);
        var existPermission = await unitOfWork.Permissions.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException("Permission is not found");

        var alreadyExistPermission = await unitOfWork.Permissions.
            SelectAsync(p => p.Action.ToLower() == model.Action.ToLower() && p.Controller.ToLower() == model.Controller.ToLower() && p.Id != id);

        if (alreadyExistPermission is not null)
            throw new AlreadyExistException("Permission is already exist");

        mapper.Map(model, existPermission);
        existPermission.Update();
        var updatePermission = await unitOfWork.Permissions.UpdateAsync(existPermission);
        await unitOfWork.SaveAsync();

        return mapper.Map<PermissionViewModel>(updatePermission);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existPermission = await unitOfWork.Permissions.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException("Permission is not found");

        await unitOfWork.Permissions.DropAsync(existPermission);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<PermissionViewModel> GetByIdAsync(long id)
    {
        var existPermission = await unitOfWork.Permissions.SelectAsync(expression: p => p.Id == id)
           ?? throw new NotFoundException("Permission is not found");

        return mapper.Map<PermissionViewModel>(existPermission);
    }

    public async Task<IEnumerable<PermissionViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var permissions = unitOfWork.Permissions.SelectAsQueryable(isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            permissions = permissions.Where(p =>
            p.Action.ToLower().Contains(search.ToLower()) ||
            p.Controller.ToLower().Contains(search.ToLower()));

        var paginatePermission = await permissions.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<PermissionViewModel>>(paginatePermission);
    }
}
