using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class HealthController : Controller
{
    [HttpGet]
    public IActionResult HealthCheck()
    {
        return Ok();
    }
}