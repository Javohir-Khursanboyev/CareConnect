using Microsoft.AspNetCore.Mvc;
using CareConnect.Service.DTOs.Assets;
using CareConnect.Service.DTOs.Doctors;
using Microsoft.AspNetCore.Authorization;
using CareConnect.Service.Configurations;
using CareConnect.Service.Services.Doctors;

namespace CareConnect.WebApi.Controllers;

public class DoctorsController(IDoctorService doctorService) : BaseController
{
    [HttpPost]
    [AllowAnonymous]
    public async ValueTask<IActionResult> PostAsync(DoctorCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await doctorService.CreateAsync(createModel)
        });
    }

    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, DoctorUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await doctorService.UpdateAsync(id, updateModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await doctorService.DeleteAsync(id)
        });
    }

    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await doctorService.GetByIdAsync(id)
        });
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetAllAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search = null)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await doctorService.GetAllAsync(@params, filter, search)
        });
    }

    [HttpPost("{id:long}/pictures/upload")]
    public async ValueTask<IActionResult> UploadPictureAsync(long id, AssetCreateModel assetCreateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await doctorService.UploadPictureAsync(id, assetCreateModel)
        });
    }

    [HttpPost("{id:long}/pictures/delete")]
    public async ValueTask<IActionResult> DeletePictureAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await doctorService.DeletePictureAsync(id)
        });
    }
}
