using Microsoft.AspNetCore.Mvc;
using CareConnect.Service.Configurations;
using CareConnect.Service.DTOs.DoctorStars;
using CareConnect.Service.Services.DoctorStars;

namespace CareConnect.WebApi.Controllers;

public class DoctorStarsController(IDoctorStarService doctorStarService) : BaseController
{
    [HttpPost()]
    public async Task<IActionResult> PostAsync(DoctorStarCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await doctorStarService.CreateAsync(createModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await doctorStarService.DeleteAsync(id)
        });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await doctorStarService.GetByIdAsync(id)
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
            Data = await doctorStarService.GetAllAsync(@params, filter, search)
        });
    }
}
