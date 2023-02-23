using Microsoft.AspNetCore.Mvc;

namespace Options.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet("index")]
    public IActionResult Index()
    {
        return Ok();
    }
}