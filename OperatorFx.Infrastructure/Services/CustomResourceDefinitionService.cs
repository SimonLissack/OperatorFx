using System.Reflection;
using k8s.Models;
using OperatorFx.Domain.Extensions;
using OperatorFx.Domain.Models;

namespace OperatorFx.Infrastructure.Services;

public class CustomResourceDefinitionService
{
    public V1CustomResourceDefinition CreateCustomResourceDefinition<T>(T instance) where T : CustomResource
    {
        var k8sEntity = instance.GetAttribute<T, KubernetesEntityAttribute>();
        var crdEntity = instance.GetAttribute<T, CustomResourceEntityAttribute>();

        var crd = new V1CustomResourceDefinition
        {
            Metadata = new V1ObjectMeta
            {
                Name = k8sEntity.Group
            },
            Spec = new V1CustomResourceDefinitionSpec
            {
                Group = k8sEntity.Group,
                Scope = crdEntity.Scope,
                Names = new V1CustomResourceDefinitionNames
                {
                    Kind = k8sEntity.Kind,
                    Singular = k8sEntity.Kind.ToLower(),
                    Plural = k8sEntity.PluralName
                },
                Versions = new List<V1CustomResourceDefinitionVersion>
                {
                    // instance.
                }
            }
        };



        return crd;
    }

}
