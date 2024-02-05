using k8s;
using Microsoft.Extensions.Options;
using OperatorFx.Infrastructure.Abstractions;
using OperatorFx.Infrastructure.Configuration;

namespace OperatorFx.Infrastructure.Services;

public class KubernetesClientFactory : IKubernetesClientFactory
{
    readonly HostingOptions _hostingOptions;
    readonly Lazy<Kubernetes> _kubernetes;

    public KubernetesClientFactory(IOptions<HostingOptions> options)
    {
        _hostingOptions = options.Value;
        _kubernetes = new Lazy<Kubernetes>(() => new Kubernetes(CreateConfiguration()));
    }

    public Kubernetes Create() => _kubernetes.Value;

    KubernetesClientConfiguration CreateConfiguration() => string.IsNullOrEmpty(_hostingOptions.KubeConfig)
        ? KubernetesClientConfiguration.BuildDefaultConfig()
        : KubernetesClientConfiguration.BuildConfigFromConfigFile(_hostingOptions.KubeConfig);
}
