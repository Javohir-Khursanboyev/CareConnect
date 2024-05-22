using Microsoft.AspNetCore.Mvc;
using CareConnect.Service.DTOs.Assets;
using CareConnect.Service.Configurations;
using Microsoft.AspNetCore.Authorization;
using CareConnect.Service.DTOs.Hospitals;
using CareConnect.Service.Services.Hospitals;

namespace CareConnect.WebApi.Controllers;

public class HospitalsController(IHospitalService hospitalService) : BaseController
{
    [HttpPost]
    [AllowAnonymous]
    public async ValueTask<IActionResult> PostAsync(HospitalCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await hospitalService.CreateAsync(createModel)
        });
    }

    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, HospitalUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await hospitalService.UpdateAsync(id, updateModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await hospitalService.DeleteAsync(id)
        });
    }

    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await hospitalService.GetByIdAsync(id)
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
            Data = await hospitalService.GetAllAsync(@params, filter, search)
        });
    }

    [HttpPost("{id:long}/pictures/upload")]
    public async ValueTask<IActionResult> UploadPictureAsync(long id, AssetCreateModel assetCreateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await hospitalService.UploadPictureAsync(id, assetCreateModel)
        });
    }

    [HttpPost("{id:long}/pictures/delete")]
    public async ValueTask<IActionResult> DeletePictureAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await hospitalService.DeletePictureAsync(id)
        });
    }
}
