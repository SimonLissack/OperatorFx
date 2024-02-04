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

public class WatcherService<T>(ILogger<WatcherService<T>> logger, IOptions<HostingOptions> hostingOptions, IKubernetes kubernetes, IPublisher publisher) : BackgroundService
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

        var response = await kubernetes.CustomObjects.ListClusterCustomObjectWithHttpMessagesAsync<T>(
            k8sAttributes.Group,
            k8sAttributes.ApiVersion,
            k8sAttributes.PluralName,
            watch: true,
            cancellationToken: stoppingToken
        );

        response.Watch(OnEventReceived);

        logger.LogInformation("Watcher service for resource type {ResourceType} stopping", typeof(T).Name);
    }

    public readonly Action<WatchEventType, T> OnEventReceived = async (e, t) =>
    {
        logger.LogInformation("Event {Event} received for resource {ResourceType}", e, t.Kind);
        await publisher.Publish(new KubernetesWatchEvent<T>
        {
            Type = e.ToString(),
            Resource = t
        });
    };
}
