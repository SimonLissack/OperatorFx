using k8s;
using MediatR;
using OperatorFx.Domain.Models;

namespace OperatorFx.Infrastructure.Eventing;

public class KubernetesWatchNotification<T> : INotification where T : CustomResource
{
    public WatchEventType Type { get; set; }
    public T Resource { get; set; } = null!;
}
