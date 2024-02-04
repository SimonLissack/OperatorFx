using k8s;
using Microsoft.Extensions.Options;
using OperatorFxNet.Infrastructure.Abstractions;
using OperatorFxNet.Infrastructure.Configuration;

namespace OperatorFxNet.Infrastructure.Services;

public class KubernetesClientFactory : IKubernetesClientFactory
{
    readonly HostingOptions _hostingOptions;
    readonly Lazy<IKubernetes> _kubernetes;

    public KubernetesClientFactory(IOptions<HostingOptions> options)
    {
        _hostingOptions = options.Value;
        _kubernetes = new Lazy<IKubernetes>(() => new Kubernetes(CreateConfiguration()));
    }

    public IKubernetes Create() => _kubernetes.Value;

    KubernetesClientConfiguration CreateConfiguration() => string.IsNullOrEmpty(_hostingOptions.KubeConfig)
        ? KubernetesClientConfiguration.BuildDefaultConfig()
        : KubernetesClientConfiguration.BuildConfigFromConfigFile(_hostingOptions.KubeConfig);
}
