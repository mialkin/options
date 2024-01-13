using Microsoft.Extensions.Options;

namespace Options.Api.BackgroundServices;

public class HostedService : BackgroundService
{
    private ApplicationSettings _applicationSettings;
    private readonly ILogger<HostedService> _logger;

    public HostedService(IOptionsMonitor<ApplicationSettings> optionsMonitor, ILogger<HostedService> logger)
    {
        _applicationSettings = optionsMonitor.CurrentValue;
        optionsMonitor.OnChange(configuration => { _applicationSettings = configuration; });
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation(_applicationSettings.Name);
            await Task.Delay(3000, stoppingToken);
        }
    }
}