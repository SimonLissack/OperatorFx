using k8s;

namespace OperatorFx.Infrastructure.Abstractions;

public interface IKubernetesClientFactory
{
    Kubernetes Create();
}
