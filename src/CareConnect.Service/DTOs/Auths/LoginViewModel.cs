using CareConnect.Service.DTOs.Users;

namespace UserApp.Service.DTOs.Auths;

public class LoginViewModel
{
    public UserViewModel User { get; set; }
    public string Token { get; set; }
}
