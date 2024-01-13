using Microsoft.Extensions.Options;
using Options.Api;
using Options.Api.BackgroundServices;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.WriteTo.Console();
});

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options => { options.DescribeAllParametersInCamelCase(); });
services.AddRouting(options => options.LowercaseUrls = true);

services
    .AddOptions<ApplicationSettings>()
    .BindConfiguration(nameof(ApplicationSettings))
    .Validate(x =>
    {
        const string optionsName = nameof(ApplicationSettings);

        if (string.IsNullOrWhiteSpace(x.Name))
            throw new OptionsValidationException(
                optionsName,
                optionsType: typeof(string),
                failureMessages: new[] { $"'{nameof(x.Name)}' property of '{optionsName}' is empty" });

        return true;
    });

services.AddHostedService<HostedService>();

var application = builder.Build();

application.UseSerilogRequestLogging();

if (application.Environment.IsDevelopment())
{
    application.UseSwagger();
    application.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

application.UseRouting();
application.MapControllers();

application.Run();