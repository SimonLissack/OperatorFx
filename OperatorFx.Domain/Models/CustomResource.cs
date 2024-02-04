using k8s;
using k8s.Models;

namespace OperatorFx.Domain.Models;

public class CustomResource : KubernetesObject, IKubernetesObject<V1ObjectMeta>
{
    public V1ObjectMeta Metadata { get; set; } = new();
}
