using AutoMapper;
using CareConnect.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using CareConnect.Service.Exceptions;
using CareConnect.Service.DTOs.Roles;
using CareConnect.Service.Extensions;
using CareConnect.Domain.Entities.Users;
using CareConnect.Service.Configurations;
using CareConnect.Service.Validators.Roles;

namespace CareConnect.Service.Services.Roles;

public class RoleService(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    RoleCreateModelValidator createModelValidator,
    RoleUpdateModelValidator updateModelValidator) : IRoleService
{
    public async Task<RoleViewModel> CreateAsync(RoleCreateModel model)
    {
        await createModelValidator.EnsureValidatedAsync(model);
        var existRole = await unitOfWork.Roles.SelectAsync(role => role.Name.ToLower() == model.Name.ToLower());
        if (existRole is not null)
            throw new AlreadyExistException("Role is already exist");

        var role = mapper.Map<Role>(model);
        role.Create();
        var createdRole = await unitOfWork.Roles.InsertAsync(role);
        await unitOfWork.SaveAsync();

        return mapper.Map<RoleViewModel>(createdRole);
    }

    public async Task<RoleViewModel> UpdateAsync(long id, RoleUpdateModel model)
    {
        await updateModelValidator.EnsureValidatedAsync(model);
        var existRole = await unitOfWork.Roles.SelectAsync(role => role.Id == id)
            ?? throw new NotFoundException("Role is not found");

        var alreadyExistRole = await unitOfWork.Roles.SelectAsync(role => role.Name.ToLower() == model.Name.ToLower() && role.Id != id);
        if (alreadyExistRole is not null)
            throw new AlreadyExistException("Role is already exist");

        mapper.Map(model, existRole);
        existRole.Update();
        var updatedRole = await unitOfWork.Roles.UpdateAsync(existRole);
        await unitOfWork.SaveAsync();

        return mapper.Map<RoleViewModel>(updatedRole);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existRole = await unitOfWork.Roles.SelectAsync(role => role.Id == id)
            ?? throw new NotFoundException("Role is not found");

        await unitOfWork.Roles.DropAsync(existRole);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async Task<RoleViewModel> GetByIdAsync(long id)
    {
        var existRole = await unitOfWork.Roles.SelectAsync(expression: role => role.Id == id)
            ?? throw new NotFoundException("Role is not found");

        return mapper.Map<RoleViewModel>(existRole);
    }

    public async Task<IEnumerable<RoleViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var roles = unitOfWork.Roles.SelectAsQueryable(isTracked: false).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            roles = roles.Where(r =>
            r.Name.ToLower().Contains(search.ToLower()));

        var paginateRoles = await roles.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<RoleViewModel>>(paginateRoles);
    }
}
