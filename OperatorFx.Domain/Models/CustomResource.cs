using k8s;
using k8s.Models;

namespace OperatorFx.Domain.Models;

public class CustomResource : KubernetesObject, IKubernetesObject<V1ObjectMeta>
{
    public V1ObjectMeta Metadata { get; set; } = new();
    public V1CustomResourceStatus Status { get; set; } = new();
}

public class V1CustomResourceStatus
{
    List<V1Condition> Conditions { get; set; } = new();
}

[AttributeUsage(AttributeTargets.Class)]
public class CustomResourceEntityAttribute(string scope = CustomResourceValues.Scope.Namespaced, bool served = true, bool storage = true) : Attribute
{
    public string Scope { get; } = scope;
    public bool Served { get; } = served;
    public bool Storage { get; } = storage;
}

public static class CustomResourceValues
{
    public static class Scope
    {
        public const string Namespaced = nameof(Namespaced);
        public const string Cluster = nameof(Cluster);
    }
}
