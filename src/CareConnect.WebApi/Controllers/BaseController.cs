using Microsoft.AspNetCore.Mvc;

namespace CareConnect.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[CustomAuthorize]
public class BaseController : ControllerBase
{

}
