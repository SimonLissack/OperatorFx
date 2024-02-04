using k8s;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OperatorFxNet.Domain.Models;
using OperatorFxNet.Infrastructure.Configuration;

namespace OperatorFxNet.Infrastructure.Services;

public class WatcherService<T>(ILogger<WatcherService<T>> logger, IOptions<HostingOptions> hostingOptions, IKubernetes kubernetes) : BackgroundService where T : CustomResource
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("starting watcher service for resource type {ResourceType}", typeof(T).Name);

        if (hostingOptions.Value.InstallCrds)
        {
            logger.LogInformation("Installing Custom Resource Definition");
        }

        throw new NotImplementedException();
    }
}
