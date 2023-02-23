using Microsoft.Extensions.Options;

namespace Options.Api.BackgroundServices;

public class HostedService : BackgroundService
{
    private ApplicationConfiguration _applicationConfiguration;
    private readonly ILogger<HostedService> _logger;

    public HostedService(IOptionsMonitor<ApplicationConfiguration> optionsMonitor, ILogger<HostedService> logger)
    {
        _applicationConfiguration = optionsMonitor.CurrentValue;
        optionsMonitor.OnChange(configuration =>
        {
            _applicationConfiguration = configuration;
        });
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation(_applicationConfiguration.Name);
            await Task.Delay(1000, stoppingToken);
        }
    }
}