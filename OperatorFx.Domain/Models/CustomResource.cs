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
