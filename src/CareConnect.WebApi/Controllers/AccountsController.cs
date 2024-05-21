using Microsoft.AspNetCore.Mvc;
using UserApp.Service.DTOs.Auths;
using CareConnect.Service.Services.Users;

namespace CareConnect.WebApi.Controllers;

public class AccountsController(IUserService userService) : ControllerBase
{
    [HttpGet("login")]
    public async Task<IActionResult> LoginAsync(LoginCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.LoginAsync(createModel)
        });
    }
}
