using Microsoft.AspNetCore.Mvc;
using CareConnect.Service.Configurations;
using CareConnect.Domain.Entities.Recommendations;
using CareConnect.Service.Services.Recommendations;

namespace CareConnect.WebApi.Controllers;

public class RecommendationsController(IRecommendationService recommendationService) : BaseController
{
    [HttpPost()]
    public async Task<IActionResult> PostAsync(RecommendationsCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await recommendationService.CreateAsync(createModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await recommendationService.DeleteAsync(id)
        });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await recommendationService.GetByIdAsync(id)
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
            Data = await recommendationService.GetAllAsync(@params, filter, search)
        });
    }
}
