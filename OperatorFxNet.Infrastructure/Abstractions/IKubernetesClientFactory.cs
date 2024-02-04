using k8s;

namespace OperatorFxNet.Infrastructure.Abstractions;

public interface IKubernetesClientFactory
{
    IKubernetes Create();
}
