using System.Reflection;
using k8s;
using Microsoft.Extensions.DependencyInjection;
using OperatorFx.Domain.Models;
using OperatorFx.Infrastructure.Abstractions;
using OperatorFx.Infrastructure.Services;

namespace OperatorFx.Infrastructure.DependencyInjection;

public static class InfrastructureInstallerExtensions
{
    public static IServiceCollection AddHosting<T>(this IServiceCollection services) where T : CustomResource
    {

        var callerAssembly = Assembly.GetCallingAssembly();
        var infrastructureAssembly = Assembly.GetAssembly(typeof(InfrastructureInstallerExtensions))!;

        services
            .AddSingleton<IKubernetesClientFactory, KubernetesClientFactory>()
            .AddSingleton<Kubernetes>(s => s.GetService<IKubernetesClientFactory>()!.Create())
            .AddMediatR(c => c
                .RegisterServicesFromAssemblies(callerAssembly, infrastructureAssembly)
            );

        return services;
    }
}
