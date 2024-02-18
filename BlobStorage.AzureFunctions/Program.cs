using BlobStorage.AzureFunctions.Models.Configs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddOptions<EmailCredentials>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(nameof(EmailCredentials)).Bind(settings);
            });
    })
    .Build();

host.Run();
