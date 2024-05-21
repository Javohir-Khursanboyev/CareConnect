namespace CareConnect.Service.DTOs.Users;

public class UserChangePasswordModel
{
    public long Id { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}