using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.Assets;
using CareConnect.Service.DTOs.Users;
using CareConnect.Service.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareConnect.WebApi.Controllers;

public class UsersController(IUserService userService) : BaseController
{
    [AllowAnonymous]
    [HttpPost()]
    public async Task<IActionResult> PostAsync(UserCreateModel userCreateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.CreateAsync(userCreateModel)
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> PutAsync(long id, UserUpdateModel userUpdateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.UpdateAsync(id, userUpdateModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.DeleteAsync(id)
        });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.GetByIdAsync(id)
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.GetAllAsync(@params, filter, search)
        });
    }

    [HttpPatch("{id:long}/change-password")]
    public async Task<IActionResult> ChangePasswordAsync(UserChangePasswordModel model)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.ChangePasswordAsync(model)
        });
    }
}
