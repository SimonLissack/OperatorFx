using System.Reflection;
using k8s.Models;
using OperatorFx.Domain.Models;

namespace OperatorFx.Domain.Extensions;

public static class CustomResourceExtensions
{
    public static TAttr GetAttribute<T, TAttr>(this T instance)
        where T : CustomResource
        where TAttr : Attribute
    {
        if (instance.GetType().GetCustomAttribute(typeof(T)) is not TAttr attribute)
        {
            throw new CustomResourceDefinitionGenerationException<T>($"The resource does not have the attribute {nameof(TAttr)}");
        }

        return attribute;
    }

    public static V1CustomResourceDefinitionVersion CreateCustomResourceDefinitionVersion<T>(this T instance)
        where T : CustomResource
    {
        var k8sEntity = instance.GetAttribute<T, KubernetesEntityAttribute>();
        var crdEntity = instance.GetAttribute<T, CustomResourceEntityAttribute>();

        var definition = new V1CustomResourceDefinitionVersion
        {
            Name = k8sEntity.ApiVersion,
            Served = crdEntity.Served,
            Storage = crdEntity.Storage
        };

        return definition;

        V1CustomResourceValidation CreateSchema()
        {
            var schema = new V1CustomResourceValidation();

            var type = instance.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(t => t.GetType() != typeof(V1ObjectMeta))
                .Where(t => t is { CanRead: true, CanWrite: true });

            foreach (var property in properties)
            {
                var propertySchema = new V1JSONSchemaProps
                {
                    Type = property.PropertyType.ToOpenApiType()
                };
            }

            return schema;
        }
    }
}
