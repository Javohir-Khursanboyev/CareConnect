using Microsoft.AspNetCore.Mvc;
using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.Permissions;
using CareConnect.Service.Services.Permissions;

namespace CareConnect.WebApi.Controllers;

public class PermissionsController(IPermissionService permissionService) : BaseController
{
    [HttpPost()]
    public async Task<IActionResult> PostAsync(PermissionCreateModel permissionCreateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await permissionService.CreateAsync(permissionCreateModel)
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> PutAsync(long id, PermissionUpdateModel permissionUpdateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await permissionService.UpdateAsync(id, permissionUpdateModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await permissionService.DeleteAsync(id)
        });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await permissionService.GetByIdAsync(id)
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
            Data = await permissionService.GetAllAsync(@params, filter, search)
        });
    }
}
