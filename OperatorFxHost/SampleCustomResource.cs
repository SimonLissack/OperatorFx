using k8s.Models;
using OperatorFx.Domain.Models;

namespace OperatorFxHost;

[KubernetesEntity(ApiVersion = "v1", Kind = "SampleCustomResource", Group = "lissack.io", PluralName = "samplecustomresources")]
public class SampleCustomResource : CustomResource
{
}
