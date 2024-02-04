using k8s;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OperatorFx.Infrastructure.Abstractions;
using OperatorFx.Infrastructure.Configuration;

namespace OperatorFx.Infrastructure.DependencyInjection;

public static class InfrastructureInstallerExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IKubernetes>(_ => new Kubernetes(KubernetesClientConfiguration.BuildDefaultConfig()));

        return services;
    }

    public static IServiceCollection AddHosting<T>(this IServiceCollection services)
    {
        services
            .AddSingleton<IKubernetesClientFactory, IKubernetesClientFactory>()
            .AddSingleton<IKubernetes>(s => s.GetService<IKubernetesClientFactory>()!.Create());

        return services;
    }
}
