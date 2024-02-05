using MediatR;
using OperatorFx.Infrastructure.Eventing;

namespace OperatorFxHost.Eventing;

public class CronTabEventHandler(ILogger<CronTabEventHandler> logger) :
    INotificationHandler<CustomResourceAdded<V1CronTab>>,
    INotificationHandler<CustomResourceModified<V1CronTab>>,
    INotificationHandler<CustomResourceDeleted<V1CronTab>>
{
    public Task Handle(CustomResourceAdded<V1CronTab> notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Event {Event} received for resource {ResourceType}", notification.Type, notification.Resource.Kind);
        return Task.CompletedTask;
    }

    public Task Handle(CustomResourceModified<V1CronTab> notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Event {Event} received for resource {ResourceType}", notification.Type, notification.Resource.Kind);
        return Task.CompletedTask;
    }

    public Task Handle(CustomResourceDeleted<V1CronTab> notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Event {Event} received for resource {ResourceType}", notification.Type, notification.Resource.Kind);
        return Task.CompletedTask;
    }
}
