using System.Reflection;
using k8s.Models;
using OperatorFxNet.Domain.Models;

namespace OperatorFxNet.Infrastructure.Services;

public class CustomResourceDefinitionService
{
    public V1CustomResourceDefinition CreateCustomResourceDefinition<T>() where T : CustomResource
    {
        var resource = typeof(T);

        if (resource.GetCustomAttribute(typeof(KubernetesEntityAttribute)) is not KubernetesEntityAttribute entityAnnotation)
        {
            throw new CustomResourceDefinitionGenerationException<T>($"The resource does not have the attribute {nameof(KubernetesEntityAttribute)}");
        }

        var crd = new V1CustomResourceDefinition
        {
            ApiVersion = entityAnnotation.ApiVersion,
            Kind = entityAnnotation.Kind,
            Metadata = new V1ObjectMeta
            {

            }
        };



        return crd;
    }
}
