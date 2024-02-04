using MediatR;
using OperatorFx.Domain.Models;

namespace OperatorFx.Infrastructure.Eventing;

public class KubernetesWatchEvent<T> : INotification where T : CustomResource
{
    public string Type { get; set; } = null!;
    public T Resource { get; set; } = null!;
}
