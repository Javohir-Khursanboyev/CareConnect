using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.Assets;
using CareConnect.Service.DTOs.Users;
using UserApp.Service.DTOs.Auths;

namespace CareConnect.Service.Services.Users;

public interface IUserService
{
    Task<UserViewModel> CreateAsync(UserCreateModel model);
    Task<UserViewModel> UpdateAsync(long id, UserUpdateModel model, bool isUsesDeleted = false);
    Task<bool> DeleteAsync(long id);
    Task<UserViewModel> GetByIdAsync(long id);
    Task<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    Task<LoginViewModel> LoginAsync(LoginCreateModel login);
    Task<UserViewModel> ChangePasswordAsync(UserChangePasswordModel model);
}