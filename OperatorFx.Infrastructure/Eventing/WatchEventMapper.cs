using k8s;
using OperatorFx.Domain.Models;

namespace OperatorFx.Infrastructure.Eventing;

public static class WatchEventMapper
{
    public static KubernetesWatchNotification<T> ToNotification<T>(this (WatchEventType, T) eventTuple) where T : CustomResource
    {
        var eventType = eventTuple.Item1;
        var resource = eventTuple.Item2;

        return eventType switch
        {
            WatchEventType.Added => new CustomResourceAdded<T>
            {
                Type = eventType,
                Resource = resource
            },
            WatchEventType.Modified => new CustomResourceModified<T>
            {
                Type = eventType,
                Resource = resource
            },
            WatchEventType.Deleted => new CustomResourceDeleted<T>
            {
                Type = eventType,
                Resource = resource
            },
            WatchEventType.Error => new CustomResourceError<T>
            {
                Type = eventType,
                Resource = resource
            },
            WatchEventType.Bookmark => new CustomResourceBookmark<T>
            {
                Type = eventType,
                Resource = resource
            },
            _ => throw new ArgumentOutOfRangeException(nameof(eventTuple))
        };
    }
}

public class CustomResourceAdded<T> : KubernetesWatchNotification<T> where T : CustomResource {}
public class CustomResourceModified<T> : KubernetesWatchNotification<T> where T : CustomResource {}
public class CustomResourceDeleted<T> : KubernetesWatchNotification<T> where T : CustomResource {}
public class CustomResourceError<T> : KubernetesWatchNotification<T> where T : CustomResource {}
public class CustomResourceBookmark<T> : KubernetesWatchNotification<T> where T : CustomResource {}
