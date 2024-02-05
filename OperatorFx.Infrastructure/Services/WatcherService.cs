using System.Reflection;
using k8s;
using k8s.Models;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OperatorFx.Domain.Models;
using OperatorFx.Infrastructure.Configuration;
using OperatorFx.Infrastructure.Eventing;

namespace OperatorFx.Infrastructure.Services;

public class WatcherService<T>(ILogger<WatcherService<T>> logger, IOptions<HostingOptions> hostingOptions, Kubernetes kubernetes, IPublisher publisher) : BackgroundService
    where T : CustomResource
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("starting watcher service for resource type {ResourceType}", typeof(T).Name);

        if (hostingOptions.Value.InstallCrds)
        {
            logger.LogInformation("Installing Custom Resource Definition");
        }

        var k8sAttributes = typeof(T).GetCustomAttribute<KubernetesEntityAttribute>()!;

        var resourceClient = new GenericClient(kubernetes, k8sAttributes.Group, k8sAttributes.ApiVersion, k8sAttributes.PluralName, disposeClient: false);

        var watcher = resourceClient.WatchAsync<T>(cancel: stoppingToken);

        await foreach (var watchEvent in watcher)
        {

            await publisher.Publish(new KubernetesWatchEvent<T>
            {
                Type = watchEvent.Item1.ToString()
            }, stoppingToken);
        }

        logger.LogInformation("Watcher service for resource type {ResourceType} stopping", typeof(T).Name);
    }
}
