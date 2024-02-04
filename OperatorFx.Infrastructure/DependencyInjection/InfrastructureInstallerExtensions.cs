using k8s;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OperatorFx.Infrastructure.Abstractions;
using OperatorFx.Infrastructure.Configuration;
using OperatorFx.Infrastructure.Services;

namespace OperatorFx.Infrastructure.DependencyInjection;

public static class InfrastructureInstallerExtensions
{
    public static IServiceCollection AddHosting<T>(this IServiceCollection services)
    {
        services
            .AddSingleton<IKubernetesClientFactory, KubernetesClientFactory>()
            .AddSingleton<IKubernetes>(s => s.GetService<IKubernetesClientFactory>()!.Create());

        return services;
    }
}
