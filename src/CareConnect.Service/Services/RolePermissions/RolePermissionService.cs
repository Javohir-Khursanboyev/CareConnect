using AutoMapper;
using CareConnect.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using CareConnect.Service.Exceptions;
using CareConnect.Service.Extensions;
using CareConnect.Domain.Entities.Users;
using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.RolePermissions;

namespace CareConnect.Service.Services.RolePermissions;

public class RolePermissionService(
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRolePermissionService
{
    public async Task<RolePermissionViewModel> CreateAsync(RolePermissionCreateModel model)
    {
        var existRole = await unitOfWork.Roles.SelectAsync(role => role.Id == model.RoleId)
            ?? throw new NotFoundException("Role is not found");
        var existPermission = await unitOfWork.Permissions.SelectAsync(permission => permission.Id == model.RoleId)
            ?? throw new NotFoundException("Permission is not found");

        var existRolePermission = await unitOfWork.RolePermissions.
            SelectAsync(rp => rp.RoleId == model.RoleId && rp.PermissionId == model.PermissionId);

        if (existRolePermission is not null)
            throw new AlreadyExistException("Role permission is already exist");

        var rolePermission = mapper.Map<RolePermission>(model);
        rolePermission.Create();
        rolePermission.Role = existRole;
        rolePermission.Permission = existPermission;

        var createdPermission = await unitOfWork.RolePermissions.InsertAsync(rolePermission);
        await unitOfWork.SaveAsync();

        return mapper.Map<RolePermissionViewModel>(createdPermission);
    }

    public async Task<RolePermissionViewModel> UpdateAsync(long id, RolePermissionUpdateModel model)
    {
        var existRolePermission = await unitOfWork.RolePermissions.SelectAsync(rp => rp.Id == id)
            ?? throw new NotFoundException("Role permission is not found");

        var existRole = await unitOfWork.Roles.SelectAsync(role => role.Id == model.RoleId)
            ?? throw new NotFoundException("Role is not found");
        var existPermission = await unitOfWork.Permissions.SelectAsync(permission => permission.Id == model.RoleId)
            ?? throw new NotFoundException("Permission is not found");

        var alreadyExistRolePermission = await unitOfWork.RolePermissions.
           SelectAsync(rp => rp.RoleId == model.RoleId && rp.PermissionId == model.PermissionId && rp.Id != id);

        if (alreadyExistRolePermission is not null)
            throw new AlreadyExistException("Role permission is already exist");

        mapper.Map(model, existRolePermission);
        existRolePermission.Update();
        existRolePermission.Role = existRole;
        existRolePermission.Permission = existPermission;

        var updatedPermission = await unitOfWork.RolePermissions.UpdateAsync(existRolePermission);
        await unitOfWork.SaveAsync();

        return mapper.Map<RolePermissionViewModel>(updatedPermission);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existRolePermission = await unitOfWork.RolePermissions.SelectAsync(rp => rp.Id == id)
            ?? throw new NotFoundException("Role permission is not found");

        await unitOfWork.RolePermissions.DropAsync(existRolePermission);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<RolePermissionViewModel> GetByIdAsync(long id)
    {
        var existRolePermission = await unitOfWork.RolePermissions.
            SelectAsync(expression: rp => rp.Id == id, includes: ["Role", "Permission"], isTracked: false)
            ?? throw new NotFoundException("Role permission is not found");

        return mapper.Map<RolePermissionViewModel>(existRolePermission);
    }

    public async Task<IEnumerable<RolePermissionViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var rolePermissions = unitOfWork.RolePermissions.
            SelectAsQueryable(includes: ["Role", "Permission"], isTracked: false).
            OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            rolePermissions = rolePermissions.Where(rp =>
            rp.Role.Name.ToLower().Contains(search.ToLower()) ||
            rp.Permission.Action.ToLower().Contains(search.ToLower()) ||
            rp.Permission.Controller.ToLower().Contains(search.ToLower()));

        var paginateRolePermissions = await rolePermissions.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<RolePermissionViewModel>>(paginateRolePermissions);
    }

    public bool CheckRolePermission(string role, string action, string controller)
    {
        var rolePermissions = unitOfWork.RolePermissions.SelectAsQueryable(expression: rp =>
            rp.Role.Name.ToLower() == role.ToLower() &&
            rp.Permission.Action.ToLower() == action.ToLower() &&
            rp.Permission.Controller.ToLower() == controller.ToLower(), isTracked: false);

        if (rolePermissions.Any()) return true;

        return false;
    }
}
