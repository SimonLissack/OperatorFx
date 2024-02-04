using MediatR;
using OperatorFx.Infrastructure.Eventing;

namespace OperatorFxHost.Eventing;

public class CronTabEventHandler(ILogger<CronTabEventHandler> logger) : INotificationHandler<KubernetesWatchEvent<V1CronTab>>
{
    public Task Handle(KubernetesWatchEvent<V1CronTab> notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Event {Event} received for resource {ResourceType}", notification.Type, notification.Resource.Kind);
        return Task.CompletedTask;
    }
}
