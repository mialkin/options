using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Options.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController(
    IOptions<ApplicationSettings> options,
    IOptionsSnapshot<ApplicationSettings> optionsSnapshot)
    : ControllerBase
{
    private readonly ApplicationSettings _settingsFromOptions = options.Value;
    private readonly ApplicationSettings _settingsFromOptionsSnapshot = optionsSnapshot.Value;

    [HttpGet("index")]
    public IActionResult Index()
    {
        var configurationFromOptions = _settingsFromOptions.Name;
        var configurationFromOptionsSnapshot = _settingsFromOptionsSnapshot.Name;

        return Ok(new { configurationFromOptions, configurationFromOptionsSnapshot });
    }
}