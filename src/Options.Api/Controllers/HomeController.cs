using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Options.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly ApplicationConfiguration _configurationFromOptions;
    private readonly ApplicationConfiguration _configurationFromOptionsSnapshot;

    public HomeController(
        IOptions<ApplicationConfiguration> options,
        IOptionsSnapshot<ApplicationConfiguration> optionsSnapshot)
    {
        _configurationFromOptions = options.Value;
        _configurationFromOptionsSnapshot = optionsSnapshot.Value;
    }

    [HttpGet("index")]
    public IActionResult Index()
    {
        var configurationFromOptions = _configurationFromOptions.Name;
        var configurationFromOptionsSnapshot = _configurationFromOptionsSnapshot.Name;

        return Ok(new { configurationFromOptions, configurationFromOptionsSnapshot });
    }
}