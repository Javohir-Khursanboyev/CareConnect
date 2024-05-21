using CareConnect.Service.DTOs.Roles;

namespace CareConnect.Service.DTOs.Users;

public class UserViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public char Gender { get; set; }
    public RoleViewModel Role { get; set; }
}
