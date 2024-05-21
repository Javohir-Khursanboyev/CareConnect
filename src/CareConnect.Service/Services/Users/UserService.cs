using AutoMapper;
using CareConnect.Data.UnitOfWorks;
using CareConnect.Domain.Entities.Users;
using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.Users;
using CareConnect.Service.Exceptions;
using CareConnect.Service.Extensions;
using CareConnect.Service.Helpers;
using Microsoft.EntityFrameworkCore;
using UserApp.Service.DTOs.Auths;

namespace CareConnect.Service.Services.Users;

public class UserService(
    IMapper mapper,
    IUnitOfWork unitOfWork) : IUserService
{
    public async Task<UserViewModel> CreateAsync(UserCreateModel model)
    {
        var existUser = await unitOfWork.Users.SelectAsync(user => user.Email == model.Email);

        if (existUser is not null && existUser.IsDeleted)
            return await UpdateAsync(existUser.Id, mapper.Map<UserUpdateModel>(model), true);

        var existRole = await unitOfWork.Roles.SelectAsync(role => role.Id == model.RoleId);
        var adminRoleId = await GetRoleIdAsync();

        model.RoleId = model.RoleId == 0 ? adminRoleId : model.RoleId;
        var mappedUser = mapper.Map<User>(model);
        mappedUser.Password = PasswordHasher.Hash(model.Password);
        mappedUser.Create();
        mappedUser.RoleId = await GetRoleIdAsync();
        var createdUser = await unitOfWork.Users.InsertAsync(mappedUser);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserViewModel>(createdUser);
    } 

    public async Task<UserViewModel> UpdateAsync(long id, UserUpdateModel model, bool isUsesDeleted = false)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id && !u.IsDeleted)
                ?? throw new NotFoundException("User is not found");

        var alreadyExistUser = await unitOfWork.Users.SelectAsync(u => u.Email == model.Email && !u.IsDeleted && u.Id != id);
        if (alreadyExistUser is not null)
            throw new AlreadyExistException("User is already exist");

        mapper.Map(model, existUser);
        existUser.Update();
        var updateModel = await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserViewModel>(updateModel);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(user => user.Id == id && !user.IsDeleted)
            ?? throw new NotFoundException("User is not found");

        existUser.Delete();
        await unitOfWork.Users.DeleteAsync(existUser);
        return true;
    }

    public async Task<UserViewModel> GetByIdAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(expression: u => u.Id == id && !u.IsDeleted, includes: ["Asset", "Role"])
            ?? throw new NotFoundException("User is not found");

        return mapper.Map<UserViewModel>(existUser);
    }

    public async Task<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var users = unitOfWork.Users.
            SelectAsQueryable(expression: u => !u.IsDeleted, includes: ["Asset", "Role"], isTracked: false).
            OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            users = users.Where(p =>
             p.FirstName.ToLower().Contains(search.ToLower()) ||
             p.LastName.ToLower().Contains(search.ToLower()));

        var paginateUsers = await users.ToPaginateAsQueryable(@params).ToListAsync();
        return mapper.Map<IEnumerable<UserViewModel>>(paginateUsers);
    }

    public async Task<LoginViewModel> LoginAsync(LoginCreateModel login)
    {
        var existUser = await unitOfWork.Users.
            SelectAsync(expression: user => user.Email == login.Email && !user.IsDeleted, includes: ["Asset", "Role"])
            ?? throw new ArgumentIsNotValidException("Email or Password is not valid");

        if (!PasswordHasher.Verify(login.Password, existUser.Password))
            throw new ArgumentIsNotValidException("Email or Password is not valid");

        var loginViewModel = new LoginViewModel
        {
            User = mapper.Map<UserViewModel>(existUser),
            Token = AuthHelper.GenerateToken(existUser)
        };
        return loginViewModel;
    }

    public async Task<UserViewModel> ChangePasswordAsync(UserChangePasswordModel model)
    {
        var existUser = await unitOfWork.Users.
            SelectAsync(expression: user => user.Id == model.Id && !user.IsDeleted, includes: ["Asset", "Role"])
            ?? throw new NotFoundException("User is not found");

        if (PasswordHasher.Verify(model.OldPassword, existUser.Password))
            throw new ArgumentIsNotValidException("Password is incorrect");

        if (model.NewPassword != model.ConfirmPassword)
            throw new ArgumentIsNotValidException("Confirm password is incorrect");

        existUser.Password = PasswordHasher.Hash(model.NewPassword);
        existUser.Update();
        await unitOfWork.SaveAsync();

        return mapper.Map<UserViewModel>(existUser);
    }

    private async Task<long> GetRoleIdAsync()
    {
        var role = await unitOfWork.Roles.SelectAsync(role => role.Name.ToLower() == Constants.AdminRoleName)
            ?? throw new NotFoundException($"Role is not found with this name {Constants.AdminRoleName}");

        return role.Id;
    }
}