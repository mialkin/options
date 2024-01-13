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

services.Configure<ApplicationSettings>(builder.Configuration.GetSection(key: nameof(ApplicationSettings)));
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